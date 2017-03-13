using System;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class AbsFunction : JmesPathFunction
    {
        public AbsFunction()
            : base("abs", 1)
        {

        }
        public override bool Validate(params JToken[] args)
        {
            var arg = args[0];
            if (arg.Type == JTokenType.Integer || arg.Type == JTokenType.Float)
                return true;
            else
                throw new Exception("invalid-type");
        }

        public override JToken Execute(params JToken[] args)
        {
            var token = args[0];

            return token.Type == JTokenType.Integer
                ? new JValue(Math.Abs(token.Value<int>()))
                : new JValue(Math.Abs(token.Value<double>()))
                ;
        }
    }
}