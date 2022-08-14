using jmespath.net.Functions.Impl;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public sealed class FindLastFunction : JmesPathFunction
    {
        public FindLastFunction()
            : base("find_last", 2, 3)
        { }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            EnsureString(args[0]);
            EnsureString(args[1]);

            if (args.Length > 2) EnsureNumbers(args[2]);

            base.Validate(args);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var text = EnsureString(args[0]);
            var search = EnsureString(args[1]);

            var position = args.Length > 2
                ? args[2].Token.Value<int>()
                : (int?)null
                ;

            return text.FindLast(search, position);
        }
    }
}