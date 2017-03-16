using System;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class ToStringFunction : JmesPathFunction
    {
        public ToStringFunction()
            : base("to_string", 1)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            if (args[0].Token.Type == JTokenType.String)
                return new JValue(args[0].Token.Value<String>());
            return new JValue(args[0].Token.ToString(Formatting.None));
        }
    }
}