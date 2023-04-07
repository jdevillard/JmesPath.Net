using jmespath.net.Functions.Impl;
using Newtonsoft.Json.Linq;
using System;

namespace DevLab.JmesPath.Functions
{
    public class FindFirstFunction : JmesPathFunction
    {
        public FindFirstFunction()
            : base("find_first", 2, 4)
        { }

        protected FindFirstFunction(string name)
            : base(name, 2, 4)
        { }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            EnsureString(args[0]);
            EnsureString(args[1]);

            // report invalid-type before invalid-value

            if (args.Length > 2) EnsureNumbers(args[2]);
            if (args.Length > 3) EnsureNumbers(args[3]);

            if (args.Length > 2) {
                var start = args[2].Token.Value<double>();
                if (!IsInteger(start))
                    throw new Exception($"Error: invalid-value, if specified, the $start parameter to the function {Name} must be an integer.");
            }

            if (args.Length > 3) {
                var count = args[3].Token.Value<double>();
                if (!IsInteger(count))
                    throw new Exception($"Error: invalid-value, if specified, the $count parameter to the function {Name} must be an integer.");
            }

            base.Validate(args);
        }

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

            return text.Find(search, start, end);
        }
    }
}