using System;
using System.Linq;
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
            var arg = args[0].Token;
            if (arg.Type != JTokenType.Object)
                throw new Exception("invalid-type");
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var token = (JObject)args[0].Token;
            return new JArray(token.Properties().Select(u => u.Value));
        }
    }
}