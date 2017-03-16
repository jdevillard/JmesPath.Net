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
            var arg = args[0].AsJToken();
            if (arg.Type == JTokenType.String)
                return new JValue(arg.Value<String>());
            return new JValue(arg.ToString(Formatting.None));
        }
    }
}