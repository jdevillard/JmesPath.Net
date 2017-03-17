using System;
using System.Linq;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class AvgFunction : JmesPathFunction
    {
        public AvgFunction()
            : base("avg", 1)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            EnsureArrayOf(args[0], "number");
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 1);
            System.Diagnostics.Debug.Assert(args[0].IsToken);

            var array = args[0].Token as JArray;
            var collection = array.Select(u => u.Value<double>());

            return collection.Any()
                ? new JValue(collection.Average())
                : JTokens.Null
                ;
        }
    }
}