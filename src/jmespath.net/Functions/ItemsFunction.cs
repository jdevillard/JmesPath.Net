using System;
using System.Collections.Generic;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ItemsFunction : JmesPathFunction
    {
        public ItemsFunction()
            : base("items", 1)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            EnsureObject(args[0]);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 1);
            System.Diagnostics.Debug.Assert(args[0].IsToken);

            var obj = args[0].Token as JObject;

            System.Diagnostics.Debug.Assert(obj != null);

            var items = new List<JArray>();

            foreach (var property in obj.Properties())
            {
                var kv = new JToken[] {
                    property.Name,
                    property.Value
                };

                items.Add(new JArray().AddRange(kv));
            }

            return new JArray().AddRange(items);
        }
    }
}