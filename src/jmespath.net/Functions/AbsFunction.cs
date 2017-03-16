using System;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class AbsFunction : JmesPathFunction
    {
        public AbsFunction()
            : base("abs", 1)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            var arg = args[0].Token;
            if (arg.Type == JTokenType.Integer || arg.Type == JTokenType.Float)
                return true;
            else
                throw new Exception("invalid-type");
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var token = args[0].Token;

            return token.Type == JTokenType.Integer
                ? new JValue(Math.Abs(token.Value<int>()))
                : new JValue(Math.Abs(token.Value<double>()))
                ;
        }
    }
}