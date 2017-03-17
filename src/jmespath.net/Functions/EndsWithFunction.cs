using System;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class EndsWithFunction : StartsEndsWithFunction
    {
        public EndsWithFunction()
            : base("ends_with")
        {

        }
        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 2);
            System.Diagnostics.Debug.Assert(args[0].IsToken);
            System.Diagnostics.Debug.Assert(args[1].IsToken);

            var text = args[0].Token.Value<String>();
            var pattern = args[1].Token.Value<String>();

            return text.EndsWith(pattern);
        }
    }
}