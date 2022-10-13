using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public sealed class LowerFunction : JmesPathFunction
    {
        public LowerFunction()
            : base("lower", 1)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            EnsureString(args[0]);
            base.Validate(args);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
            => EnsureString(args[0]).ToLowerInvariant();
    }
}