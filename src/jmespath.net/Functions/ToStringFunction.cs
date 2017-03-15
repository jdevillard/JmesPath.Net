using System;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ToStringFunction : JmesPathFunction
    {
        public ToStringFunction()
            : base("to_string", 1)
        {

        }
        public override bool Validate(params JToken[] args)
        {
            return true;
        }

        public override JToken Execute(params JToken[] args)
        {
            if (args[0].Type == JTokenType.String)
                return new JValue(args[0].Value<String>());
            return new JValue(args[0].ToString(Formatting.None));
        }
    }
}