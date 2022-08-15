using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;
using System;

namespace DevLab.JmesPath.Functions
{
    public class PadLeftFunction : JmesPathFunction
    {
        protected string text_;
        protected int width_;
        protected char character_ = ' ';

        public PadLeftFunction()
            : this("pad_left")
        {
        }

        protected PadLeftFunction(string name)
            : base(name, 2, 3)
        { }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            text_ = EnsureString(args[0]);

            var width = args[1].Token.Value<double>();
            if (args[1].Token.GetTokenType() != "number" || width < 0.0 || !IsInteger(width))
                throw new Exception($"Error: syntax, if specified, the $width parameter to the function {Name} must be a positive integer.");

            width_ = Convert.ToInt32(width);

            if (args.Length > 2)
            {
                var character = EnsureString(args[2]);
                if (character.Length > 1)
                    throw new Exception($"Error: syntax, function {Name} expects its third argument to be a string with a single character at most.");

                character_ = character[0];
            }

            base.Validate(args);
        }
        public override JToken Execute(params JmesPathFunctionArgument[] args)
            => text_.PadLeft(width_, character_);
    }
}