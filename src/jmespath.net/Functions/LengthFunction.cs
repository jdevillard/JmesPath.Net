using System;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class LengthFunction : JmesPathFunction
    {
        public LengthFunction()
            : base("length", 1)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();

            var type = args[0].Token.GetTokenType();
            if (type != "array" && type != "object" && type != "string")
                throw new Exception($"Error: invalid-type, function {Name} expects either an object, an array or a string.");
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var token = args[0].Token;

            switch (token.GetTokenType())
            {
                case "string":
                    return token.Value<String>().Length;               
                case "array":
                    return ((JArray) token).Count;
                case "object":
                    return ((JObject) token).Count;
                default:
                    return 0;
            }
        }
    }
}