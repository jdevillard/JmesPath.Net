using System;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class CeilFunction : JmesPathFunction
    {
        public CeilFunction()
            : base("ceil", 1)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            EnsureNumbers(args);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 1);
            System.Diagnostics.Debug.Assert(args[0].IsToken);

            var argument = args[0];
            var token = argument.Token;

            return new JValue(Convert.ToInt32(Math.Ceiling(token.Value<double>())));
        }
    }
}