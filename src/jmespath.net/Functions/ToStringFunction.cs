using System;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ToStringFunction : JmesPathFunction
    {
        public ToStringFunction()
            : base("to_string", 1)
        {

        }
        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 1);
            System.Diagnostics.Debug.Assert(args[0].IsToken);

            var argument = args[0];
            var token = argument.Token;

            return token.GetTokenType() == "string" 
                ? token 
                : new JValue(token.AsString())
                ;
        }
    }
}