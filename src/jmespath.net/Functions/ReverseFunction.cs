using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class ReverseFunction : JmesPathFunction
    {
        public ReverseFunction()
            : base("reverse", 1)
        {

        }

        public override bool Validate(params JmesPathArgument[] args)
        {
            var arg = args[0].AsJToken();
            if (arg.Type == JTokenType.String || arg.Type == JTokenType.Array)
                return true;
            else
                throw new Exception("invalid-type");
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var arg = args[0].AsJToken();
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