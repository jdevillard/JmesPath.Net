using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public sealed class PadRightFunction : PadLeftFunction {
        public PadRightFunction()
            : base("pad_right")
        { }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
            => text_.PadRight(width_, character_);
    }
}