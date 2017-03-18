using System;
using System.Linq;
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
            var array = (JArray)args[0].Token;

            if (array.Count == 0)
                return new JArray();

            var item = array[0];

            if (item.Type == JTokenType.Float)
                return new JArray().AddRange(Sort<double>(array));
            else if (item.Type == JTokenType.Integer)
                return new JArray().AddRange(Sort<int>(array));
            else
                return new JArray().AddRange(Sort<string>(array));
        }

        private static JToken[] Sort<T>(JArray array)
        {
            return array
                .OrderBy(u => u.Value<T>())
                .ToArray();
        }
    }
}