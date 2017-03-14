using System;
using System.Collections.Generic;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathFilterProjection : JmesPathProjection
    {
        private readonly JmesPathExpression expression_;

        public JmesPathFilterProjection(JmesPathExpression expression)
        {
            expression_ = expression;
        }

        public override JmesPathArgument Project(JmesPathArgument argument)
        {
            if (argument.IsProjection)
                argument = argument.AsJToken();

            var array = argument.Token as JArray;
            if (array == null)
                return null;

            var items = new List<JmesPathArgument>();

            foreach (var item in array)
            {
                var result = expression_.Transform(item);
                if (!JmesPathArgument.IsFalse(result))
                    items.Add(item);
            }

            return new JmesPathArgument(items);
        }

        public override void Accept(IVisitor visitor)
        {
            base.Accept(visitor);
            expression_.Accept(visitor);
        }
    }
}