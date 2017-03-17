using System;
using System.Globalization;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ToNumberFunction : JmesPathFunction
    {
        public ToNumberFunction()
            : base("to_number", 1)
        {
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 1);
            System.Diagnostics.Debug.Assert(args[0].IsToken);

            var argument = args[0];
            var token = argument.Token;

            switch (token.GetTokenType())
            {
                case "number":
                    return token;

                case "string":
                    {
                        var value = token.Value<string>();

                        int i = 0;
                        double d = 0;

                        if (int.TryParse(value, out i))
                            return new JValue(i);
                        if (double.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out d))
                            return new JValue(d);

                        return JTokens.Null;
                    }

                default:
                    return JTokens.Null;
            }
        }
    }
}