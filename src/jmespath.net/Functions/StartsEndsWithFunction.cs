using System;
using DevLab.JmesPath.Utils;

namespace DevLab.JmesPath.Functions
{
    public abstract class StartsEndsWithFunction : JmesPathFunction
    {
        protected StartsEndsWithFunction(string name)
            : base(name, 2)
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
    }
}