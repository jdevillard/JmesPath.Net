using jmespath.net.Functions.Impl;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public sealed class FindFirstFunction : JmesPathFunction
    {
        public FindFirstFunction()
            : base("find_first", 2, 4)
        { }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            EnsureString(args[0]);
            EnsureString(args[1]);

            if (args.Length > 2) EnsureNumbers(args[2]);
            if (args.Length > 3) EnsureNumbers(args[3]);

            base.Validate(args);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var text = EnsureString(args[0]);
            var search = EnsureString(args[1]);

            var start = args.Length > 2
                ? args[2].Token.Value<int>()
                : (int?)null
                ;

            var end = args.Length > 3
                ? args[3].Token.Value<int>()
                : (int?)null
                ;

            return text.Find(search, start, end);
        }
    }
}