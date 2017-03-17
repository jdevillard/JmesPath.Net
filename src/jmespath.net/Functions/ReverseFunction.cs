using System;
using System.Linq;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ReverseFunction : JmesPathFunction
    {
        public ReverseFunction()
            : base("reverse", 1)
        {

        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            var arg = args[0].Token;
            var tokenType = arg.GetTokenType();
            if (tokenType != "string" && tokenType != "array")
                throw new Exception($"Error: invalid-type, function {Name} accepts either an array or a string.");
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var token = args[0].Token;
            switch (token.GetTokenType())
            {
                case "string":
                {
                    var characters = token.Value<String>().Reverse().ToArray();
                    return new JValue(new string(characters));
                }
                case "array":
                {
                        var items = ((JArray)token).Reverse();
                    return new JArray().AddRange(items);
                }
                default:
                    return null;
            }
        }
    }
}