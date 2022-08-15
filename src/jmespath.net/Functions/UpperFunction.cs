using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public sealed class UpperFunction : JmesPathFunction
    {
        public UpperFunction()
            : base("upper", 1)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            EnsureString(args[0]);
            base.Validate(args);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
            => EnsureString(args[0]).ToUpperInvariant();
    }
}