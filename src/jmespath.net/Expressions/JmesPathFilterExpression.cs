using System;
using System.Collections.Generic;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathFilterExpression : JmesPathExpression
    {
        private readonly JmesPathExpression expression_;

        public JmesPathFilterExpression(JmesPathExpression expression)
        {
            expression_ = expression;
        }

        protected override JmesPathArgument Transform(JToken json)
        {
            var array = json as JArray;
            if (array == null)
                return null;

            var items = new List<JToken>();

            foreach (var item in array)
            {
                var result = expression_.Transform(item);
                if (!JmesPathArgument.IsFalse(result))
                    items.Add(item);
            }

            return new JArray().AddRange(items);
        }
    }
}