using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class KeysFunction : JmesPathFunction
    {
        public KeysFunction()
            : base("keys", 1)
        {

        }
        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();

            var arg = args[0].Token;
            if (arg.Type != JTokenType.Object)
                throw new Exception("invalid-type");
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var token = (JObject)args[0].Token;
            return new JArray(token.Properties().Select(u => u.Name));
        }
    }
}