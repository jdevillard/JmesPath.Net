using System;
using System.Linq;
using System.Linq.Expressions;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class SortFunction : JmesPathFunction
    {
        public SortFunction()
            : base("sort", 1)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            var list = args[0].AsJToken();
            if(list.Type != JTokenType.Array)
                throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var list = (JArray)(args[0].AsJToken()).AsJEnumerable();
            var ordered = list.OrderBy(u => u.Value<String>()).ToArray();
            return new JArray(ordered.AsJEnumerable());
        }
    }
}