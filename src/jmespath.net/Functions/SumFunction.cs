using System;
using System.Linq;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class SumFunction : JmesPathFunction
    {
        public SumFunction()
            : base("sum", 1)
        {

        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            EnsureArrayOf(args[0], "number");
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var arg = (JArray)args[0].Token;

            if (arg.Count == 0)
                return new JValue(0);

            var s= arg.Select(u => u?.Value<double>() ?? 0);
            return s.Any() ? new JValue(s.Sum()) : JTokens.Null;
        }
    }
}