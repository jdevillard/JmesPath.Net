using jmespath.net.Functions.Impl;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public sealed class FindLastFunction : FindFirstFunction
    {
        public FindLastFunction()
            : base("find_last")
        { }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var text = EnsureString(args[0]);
            var search = EnsureString(args[1]);

            if (text.Length == 0 || search.Length == 0)
                return null;

            var start = args.Length > 2
                ? args[2].Token.Value<int>()
                : (int?)null
                ;

            var end = args.Length > 3
                ? args[3].Token.Value<int>()
                : (int?)null
                ;

            return text.FindLast(search, start, end);
        }
    }
}