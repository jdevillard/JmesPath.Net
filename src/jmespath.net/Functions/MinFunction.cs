using System;
using System.Linq;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class MinFunction : MathArrayFunction
    {
        public MinFunction()
            : base("min", 1)
        {
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var array = (JArray) args[0].Token;

            if (array.Count == 0)
                return JTokens.Null;

            var item = array[0];
            var type = item.GetTokenType();

            switch (type)
            {
                case "number":
                {
                    if (item.Type == JTokenType.Float)
                        return GetMin<double>(array);

                    else /* if (token.Type == JTokenType.Integer) */
                        return GetMin<int>(array);
                }

                case "string":
                    return GetMin<string>(array);

                default:
                    System.Diagnostics.Debug.Assert(false);
                    throw new NotSupportedException("Error: invalid-type");
            }
        }

        private static JToken GetMin<T>(JArray array)
        {
            var sequence = array
                .Select(u => u.Value<T>())
                .ToArray()
                ;

            return sequence.Length > 0
                ? new JValue(sequence.Min())
                : null
                ;
        }
    }
}