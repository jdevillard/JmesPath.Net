using DevLab.JmesPath.Utils;
using jmespath.net.Functions.Impl;
using Newtonsoft.Json.Linq;
using System;

namespace DevLab.JmesPath.Functions
{
    public sealed class ReplaceFunction : JmesPathFunction
    {
        public ReplaceFunction()
            : base("replace", 3, 4)
        { }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            EnsureString(args[0]);
            EnsureString(args[1]);
            EnsureString(args[2]);

            if (args.Length > 3)
            {
                var count = args[3].Token.Value<double>();
                if (args[3].Token.GetTokenType() != "number" || count < 0.0 || !IsInteger(count))
                    throw new Exception($"Error: syntax, if specified, the $count parameter to the function {Name} must be a positive integer.");
            }

            base.Validate(args);
        }
        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var text = EnsureString(args[0]);
            var replace = EnsureString(args[1]);
            var with = EnsureString(args[2]);

            var count = (args.Length > 3)
                ? Convert.ToInt32(args[3].Token.Value<double>())
                : (int?)null
                ;

            return text.Replace(replace, with, count);
        }
    }
}