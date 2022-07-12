using System;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathLiteral : JmesPathExpression
    {
        private readonly JToken value_;

        public JmesPathLiteral(JToken value)
        {
            value_ = value;
        }

        public JToken Value => value_;

        protected override JmesPathArgument Transform(JToken json)
        {
            return value_;
        }

        public override string ToString()
            => StringUtil.WrapLiteral(value_.AsString());
    }
}