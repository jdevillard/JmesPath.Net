using System;
using System.Linq;
using System.Linq.Expressions;
using DevLab.JmesPath.Interop;
using DevLab.JmesPath.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class SortFunction : JmesPathFunction
    {
        public SortFunction()
            : base("sort", 1)
        {

        }
        public override bool Validate(params JToken[] args)
        {
            var list = args[0];
            if(list.Type != JTokenType.Array)
                throw new Exception("invalid-type");

            return true;
        }

        public override JToken Execute(params JToken[] args)
        {
            var list = (JArray)args[0].AsJEnumerable();
            var ordered = list.OrderBy(u => u.Value<String>()).ToArray();
            return new JArray(ordered.AsJEnumerable());
        }
    }
}