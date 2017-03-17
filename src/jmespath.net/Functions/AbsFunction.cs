using System;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class AbsFunction : JmesPathFunction
    {
        public AbsFunction()
            : base("abs", 1)
        {

        }
        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            EnsureNumbers(args[0]);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 1);
            System.Diagnostics.Debug.Assert(args[0].IsToken);

            var token = args[0].Token;

            return token.Type == JTokenType.Integer
                ? new JValue(Math.Abs(token.Value<int>()))
                : new JValue(Math.Abs(token.Value<double>()))
                ;
        }
    }
}