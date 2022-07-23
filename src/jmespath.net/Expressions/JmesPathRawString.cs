﻿using DevLab.JmesPath.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathRawString : JmesPathExpression
    {
        public JmesPathRawString(string value)
        {
            Value = value;
        }

        public string Value { get; }

        protected override JmesPathArgument Transform(JToken json)
        {
            return new JValue(Value);
        }

        public override string ToString()
            => StringUtil.WrapRawString(Value);
    }
}