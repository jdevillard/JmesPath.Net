using System;
using System.Collections.Generic;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ZipFunction : JmesPathFunction
    {
        public ZipFunction()
            : base("zip", 1, true)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();

            foreach (var item in args)
                EnsureArray(item);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            // determines how many items are present
            // for each array of the result

            var count = Int32.MaxValue;

            foreach (var argument in args)
            {
                var array = argument.Token as JArray;
                System.Diagnostics.Debug.Assert(array != null);
                count = Math.Min(count, array.Count);
            }

            var items = new JToken[count];

            for (var index = 0; index < count; index++)
            {
                var nth = new List<JToken>();

                foreach (var argument in args)
                {
                    var array = argument.Token as JArray;
                    System.Diagnostics.Debug.Assert(array != null);
                    nth.Add(array[index]);
                }

                items[index] = new JArray().AddRange(nth);
            }

            return new JArray().AddRange(items);
        }
    }
}