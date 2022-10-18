using System;
using System.Collections.Generic;
using System.Linq;
using DevLab.JmesPath.Expressions;
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

            if (array.Count == 0)
                return new JArray();

            // make sure this is an homogeneous array
            // with all items from a single expected type

            var keyCollection = array
                .Select(u => expression.Transform(u).AsJToken())
                .ToArray()
                ;

            var actualItemType = keyCollection[0].GetTokenType();
            if (actualItemType != "number" && actualItemType != "string")
                throw new Exception($"Error: invalid-type, the expression argument of function {Name} should return a number or a string.");

            if (keyCollection.Any(k => k.GetTokenType() != actualItemType))
                throw new Exception($"Error: invalid-type, all items resulting from the evaluation of the expression argument of function {Name} should have the same type.");

            // sort array

            var tokens = array.AsEnumerable().ToArray();
            JToken[] ordered = tokens;

            if (actualItemType == "number")
            {
                var actualKeyTokenType = keyCollection[0].Type;
                if (actualKeyTokenType == JTokenType.Float)
                    ordered = SortByNumbers<double>(tokens, expression);
                else if (actualKeyTokenType == JTokenType.Integer)
                    ordered = SortByNumbers<int>(tokens, expression);
            }
            else
            {
                 ordered = SortByText(tokens, expression);
            }

            return new JArray(ordered);
        }

        private JToken[] SortByNumbers<T>(JToken[] array, JmesPathExpression expression)
        {
            T keySelector(JToken t) {
                var token = expression.Transform(t).AsJToken();
                return token.Value<T>();
            };

            var ordered = array
                .OrderBy(keySelector)
                .ToArray()
                ;

            return ordered;
        }
        private JToken[] SortByText(JToken[] array, JmesPathExpression expression)
        {
            Text keySelector(JToken t)
            {
                var key = expression.Transform(t).AsJToken();
                return (Text) key.Value<string>();
            };
            IComparer<Text> comparer = Text.CodePointComparer;

            var ordered = array
                .OrderBy(
                    keySelector,
                    comparer
                )
                .ToArray()
                ;

            return ordered;
        }
    }
}