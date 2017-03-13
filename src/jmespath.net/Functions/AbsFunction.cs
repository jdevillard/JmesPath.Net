using System;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class AbsFunction : JFunction
    {
        public AbsFunction():base("abs")
        {

        }
        public override bool Validate(params JToken[] args)
        {
            if (args == null || args.Length != 1)
                throw new Exception("invalid-arity");

            var arg = args[0];
            if(arg.Type == JTokenType.Integer || arg.Type ==JTokenType.Float)
                return true;
            else
                throw new Exception("invalid-type");
        }

        public override JToken Execute(params JToken[] args)
        {
            var jToken = args[0];
            if(jToken.Type == JTokenType.Integer)
                return new JValue(Math.Abs(jToken.Value<Int32>()));
            else
                return new JValue(Math.Abs(jToken.Value<double>()));
        }
    }
}