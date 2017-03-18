using System;
using System.Linq;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class SortByFunction : ByFunction
    {
        public SortByFunction()
            : base("sort_by")
        {
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 2);
            System.Diagnostics.Debug.Assert(args[0].IsToken);
            System.Diagnostics.Debug.Assert(args[1].IsExpressionType);

            var array = (JArray)args[0].Token;
            var expression = args[1].Expression;

            var done = false;

            var expectedItemType = "none";

            var ordered = array.OrderBy(u =>
            {
                var e = expression.Transform(u);

                var actualItemType = e.AsJToken().GetTokenType();

                if (!done)
                {
                    if (actualItemType != "number" && actualItemType != "string")
                        throw new Exception($"Error: invalid-type, the expression argument of function {Name} should return a number or a string.");

                    expectedItemType = actualItemType;
                    done = true;
                }

                if (expectedItemType != actualItemType)
                    throw new Exception("Error: invalid-type, all items resulting from the evaluation of the expression argument of function {Name} should have the same type.");
                                
                return e.AsJToken();

            }).ToArray();

            return new JArray()
                .AddRange(ordered)
                ;
        }
    }
}