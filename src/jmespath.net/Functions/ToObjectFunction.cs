using System;
using System.Collections.Generic;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ToObjectFunction : JmesPathFunction
    {
        public ToObjectFunction()
            : base("to_object", 1)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();

            var array = EnsureArray(args[0]);
            foreach (var item in array)
            {
                var nestedArray = EnsureArray(new JmesPathFunctionArgument(item));
                if (nestedArray.Count != 2)
                    throw new Exception("Error: invalid-value, the function {Name} expects an array containing arrays of string and value pairs.");
                if (nestedArray[0].GetTokenType() != "string")
                    throw new Exception("Error: invalid-value, the function {Name} expects an array containing arrays of string and value pairs.");
            }
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 1);
            System.Diagnostics.Debug.Assert(args[0].IsToken);

            var array = args[0].Token as JArray;

            System.Diagnostics.Debug.Assert(array != null);

            var properties = new List<JProperty>();

            foreach (var item in array)
            {
                var nested = item as JArray;
                System.Diagnostics.Debug.Assert(nested != null);
                System.Diagnostics.Debug.Assert(nested.Count == 2);

                var name = nested[0].Value<string>();
                var value = nested[1];

                properties.Add(new JProperty(name, value));
            }

            return new JObject(properties);
        }
    }
}