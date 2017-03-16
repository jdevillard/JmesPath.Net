using System;
using System.Linq;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class KeysFunction : JmesPathFunction
    {
        public KeysFunction()
            : base("keys", 1)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            var arg = args[0].AsJToken();
            if (arg.Type != JTokenType.Object)
                throw new Exception("invalid-type");
            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var token = (JObject)args[0].AsJToken();
            return new JArray(token.Properties().Select(u => u.Name));
        }
    }
}