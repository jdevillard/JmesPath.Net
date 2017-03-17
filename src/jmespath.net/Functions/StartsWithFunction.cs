using System;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class StartsWithFunction : JmesPathFunction
    {
        public StartsWithFunction()
            : base("starts_with", 2)
        {

        }
        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();

            var text = args[0].Token.GetTokenType();
            var pattern = args[1].Token.GetTokenType();

            if (text != "string" || (pattern != "string" && pattern != "null"))
                throw new Exception($"Error: invalid-type, both arguments to function {Name} must be strings.");
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 2);
            System.Diagnostics.Debug.Assert(args[0].IsToken);
            System.Diagnostics.Debug.Assert(args[1].IsToken);

            var text = args[0].Token.Value<String>();
            var pattern = args[1].Token.Value<String>();

            return text.StartsWith(pattern);
        }
    }
}