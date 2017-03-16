using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class SumFunction : JmesPathFunction
    {
        public SumFunction()
            : base("sum", 1)
        {

        }

        public override bool Validate(params JmesPathArgument[] args)
        {
            if (args[0].AsJToken().Type != JTokenType.Array)
                throw new Exception("invalid-type");

            foreach (var item in (JArray)args[0].AsJToken())
                if (item.Type != JTokenType.Integer
                    && item.Type != JTokenType.Float)
                    throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var arg = ((JArray)(args[0].AsJToken()));
            if(arg.Count == 0)
                return new JValue(0);

            var s= arg.Select(u => u?.Value<double>() ?? 0);
            return s.Any() ? new JValue(s.Sum()) : null;
        }
    }
}