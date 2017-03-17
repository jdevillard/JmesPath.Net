using System;
using System.Linq;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ValuesFunction : JmesPathFunction
    {
        public ValuesFunction()
            : base("values", 1)
        {
        }
        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            EnsureObject(args[0]);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var token = (JObject)args[0].Token;

            var items = token
                .Properties()
                .Select(u => u.Value)
                ;

            return new JArray().AddRange(items);
        }
    }
}