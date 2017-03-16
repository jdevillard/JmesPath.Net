using System;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class FloorFunction : JmesPathFunction
    {
        public FloorFunction()
            : base("floor", 1)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            var arg = args[0].AsJToken();
            if (arg.Type == JTokenType.Integer || arg.Type == JTokenType.Float)
                return true;
            else
                throw new Exception("invalid-type");
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var token = args[0].AsJToken();

            return new JValue(Convert.ToInt32(Math.Floor(token.Value<double>())));
        }
    }
}