using System;
using DevLab.JmesPath.Utils;
using jmespath.net.Functions.Impl;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class SplitFunction : JmesPathFunction
    {
        private string text_;
        private string separator_;
        private int? count_;

        public SplitFunction()
            : base("split", 2, 3)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            text_ = EnsureString(args[0]);
            separator_ = EnsureString(args[1]);

            if (args.Length > 2)
            {
                var count = args[2].Token.Value<double>();
                if (args[2].Token.GetTokenType() != "number" || count < 0.0 || !IsInteger(count))
                    throw new Exception($"Error: syntax, if specified, the $count parameter to the function {Name} must be a positive integer.");

                count_ = Convert.ToInt32(count);
            }

            base.Validate(args);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
            => JArray.FromObject(text_.Split(separator_, count_));
    }
}