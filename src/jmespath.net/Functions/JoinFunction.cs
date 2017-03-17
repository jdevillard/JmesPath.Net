using System;
using System.Linq;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class JoinFunction : JmesPathFunction
    {
        public JoinFunction()
            : base("join", 2)
        {

        }
        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();

            var separator = args[0].Token.GetTokenType();
            var array = args[1].Token.GetTokenType();

            if (separator != "string" || array != "array")
                throw new Exception($"Error: invalid-type, function {Name} expects a string separator and an array of strings.");

            EnsureArrayOf(args[1], "string");
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var glue = args[0].Token.Value<String>();
            var array = (JArray)args[1].Token;

            var strings = array.Select(u => u.Value<String>());
            var joined = String.Join(glue,strings);

            return new JValue(joined);
        }
    }
}