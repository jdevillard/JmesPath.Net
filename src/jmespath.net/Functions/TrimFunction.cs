using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class TrimFunction : JmesPathFunction
    {
        protected string text_;
        protected char[] characters_ = null;

        public TrimFunction()
            : this("trim")
        { }

        protected TrimFunction(string name)
            : base(name, 1, 2)
        { }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            text_ = EnsureString(args[0]);
            if (args.Length > 1)
                characters_ = (EnsureString(args[1])).ToCharArray();

            base.Validate(args);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
            => text_.Trim(characters_);
    }

    public sealed class TrimLeftFunction : TrimFunction
    {
        public TrimLeftFunction()
            : base("trim_left")
        { }
        public override JToken Execute(params JmesPathFunctionArgument[] args)
            => text_.TrimStart(characters_);
    }
    public sealed class TrimRightFunction : TrimFunction
    {
        public TrimRightFunction()
            : base("trim_right")
        { }
        public override JToken Execute(params JmesPathFunctionArgument[] args)
            => text_.TrimEnd(characters_);
    }
}