using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class SortFunction : JmesPathFunction
    {
        public SortFunction()
            : base("sort", 1)
        {

        }
        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            EnsureArrayOfSame(args[0]);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var list = (JArray)args[0].Token;
            var ordered = list
                .OrderBy(u => u.Value<String>())
                .ToArray();
            return new JArray().AddRange(ordered);
        }
    }
}