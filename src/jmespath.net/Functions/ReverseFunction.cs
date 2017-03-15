using System;
using System.Linq;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ReverseFunction : JmesPathFunction
    {
        public ReverseFunction()
            : base("reverse", 1)
        {

        }

        public override bool Validate(params JToken[] args)
        {
            var arg = args[0];
            if (arg.Type == JTokenType.String || arg.Type == JTokenType.Array)
                return true;
            else
                throw new Exception("invalid-type");
        }

        public override JToken Execute(params JToken[] args)
        {
            var arg = args[0];
            switch (arg.Type)
            {
                case JTokenType.String:
                    return new JValue(new String(arg.Value<String>().Reverse().ToArray()));
                case JTokenType.Array:
                    return new JArray(((JArray) (arg)).AsJEnumerable().Reverse());
                default:
                    return null;
            }
        }
    }
}