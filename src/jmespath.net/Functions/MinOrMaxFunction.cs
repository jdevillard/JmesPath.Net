using System;
using System.Collections.Generic;
using System.Linq;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public abstract class MinOrMaxFunction : MathArrayFunction
    {
        public MinOrMaxFunction(string name)
            : base(name, 1)
        { }

        protected override JToken Execute(JArray array, IEnumerable<string> dataTypes)
        {
            var item = array[0];
            var type = item.GetTokenType();

            switch (type)
            {
                case "number":
                    {
                        // special case if all items in the array are integers

                        var tokenTypes = array.Select(x => x.Type).Distinct().ToArray();
                        if (tokenTypes.Length == 1)
                        {
                            if (tokenTypes[0] == JTokenType.Integer)
                                return GetMinOrMax<long>(array);
                        }

                        return GetMinOrMax<double>(array);
                    }

                case "string":
                    return GetMinOrMax<string>(array);

                default:
                    System.Diagnostics.Debug.Assert(false);
                    throw new NotSupportedException("Error: invalid-type");
            }
        }

        protected abstract JToken GetMinOrMax<T>(JArray array);
    }
}