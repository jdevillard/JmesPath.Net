using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class AvgFunction : JmesPathFunction
    {
        public AvgFunction()
            : base("avg", 1)
        {

        }

        public override bool Validate(params JmesPathArgument[] args)
        {
            if (args[0].AsJToken().Type != JTokenType.Array)
                throw new Exception("invalid-type");

            foreach (var item in (JArray)args[0].AsJToken())
                if (item.Type != JTokenType.Integer
                    && item.Type != JTokenType.Float                    )
                    throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var s = ((JArray)(args[0].AsJToken()))
                .Select(u => u.Value<double>());
            return s.Any() ? new JValue(s.Average()) : null;
        }
    }
}