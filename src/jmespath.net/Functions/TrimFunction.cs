using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class TrimFunction : JmesPathFunction
    {
        public TrimFunction()
            : this("trim")
        { }

        protected TrimFunction(string name)
            : base(name, 1, 2)
        { }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            EnsureString(args[0]);
            if (args.Length > 1)
                (EnsureString(args[1])).ToCharArray();

            base.Validate(args);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var (Text, Characters) = GetFunctionArguments(args);
            return Text.Trim(Characters);
        }

        protected (string Text, char[] Characters) GetFunctionArguments(JmesPathFunctionArgument[] args)
            => (EnsureString(args[0]), args.Length > 1 ? EnsureString(args[1]).ToCharArray() : null);
    }

    public sealed class TrimLeftFunction : TrimFunction
    {
        public TrimLeftFunction()
            : base("trim_left")
        { }
        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var (Text, Characters) = GetFunctionArguments(args);
            return Text.TrimStart(Characters);
        }
    }
    public sealed class TrimRightFunction : TrimFunction
    {
        public TrimRightFunction()
            : base("trim_right")
        { }
        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var (Text, Characters) = GetFunctionArguments(args);
            return Text.TrimEnd(Characters);
        }
    }
}