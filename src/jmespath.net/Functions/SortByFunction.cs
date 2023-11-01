using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public override async Task<JToken> ExecuteAsync(params JmesPathFunctionArgument[] args)
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

            var keyCollection = await Task.WhenAll(
                    array.Select(async u => (await expression.TransformAsync(u)).AsJToken())
                    .ToArray())
                ;

            var actualItemType = keyCollection[0].GetTokenType();
            if (actualItemType != "number" && actualItemType != "string")
                throw new Exception(
                    $"Error: invalid-type, the expression argument of function {Name} should return a number or a string.");

            if (keyCollection.Any(k => k.GetTokenType() != actualItemType))
                throw new Exception(
                    $"Error: invalid-type, all items resulting from the evaluation of the expression argument of function {Name} should have the same type.");

            // sort array

            var tokens = array.AsEnumerable().ToArray();
            JToken[] ordered = tokens;

            if (actualItemType == "number")
            {
                var actualKeyTokenType = keyCollection[0].Type;
                if (actualKeyTokenType == JTokenType.Float)
                    ordered = await SortByNumbersAsync<double>(tokens, expression);
                else if (actualKeyTokenType == JTokenType.Integer)
                    ordered = await SortByNumbersAsync<int>(tokens, expression);
            }
            else
            {
                ordered = await SortByTextAsync(tokens, expression);
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

        private async Task<JToken[]> SortByNumbersAsync<T>(JToken[] array, JmesPathExpression expression)
        {
            async Task<T> KeySelectorAsync(JToken t)
            {
                var token = (await expression.TransformAsync(t)).AsJToken();
                return token.Value<T>();
            }

            // Asynchronously project array to key-value pairs
            var keyValuePairs = new List<KeyValuePair<T, JToken>>();
            foreach (var token in array)
            {
                var key = await KeySelectorAsync(token);
                keyValuePairs.Add(new KeyValuePair<T, JToken>(key, token));
            }

            var sortedKeyValuePairs = keyValuePairs.OrderBy(kv => kv.Key).ToList();
            return sortedKeyValuePairs.Select(kv => kv.Value).ToArray();
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
        
        private async Task<JToken[]> SortByTextAsync(JToken[] array, JmesPathExpression expression)
        {
            async Task<Text> KeySelectorAsync(JToken t)
            {
                var key = (await expression.TransformAsync(t)).AsJToken();
                return (Text)key.Value<string>();
            }
            IComparer<Text> comparer = Text.CodePointComparer;

            // Asynchronously project array to key-value pairs
            var keyValuePairs = new List<KeyValuePair<Text, JToken>>();
            foreach (var token in array)
            {
                var key = await KeySelectorAsync(token);
                keyValuePairs.Add(new KeyValuePair<Text, JToken>(key, token));
            }
            
            var sortedKeyValuePairs = keyValuePairs.OrderBy(kv => kv.Key).ToList();
            return sortedKeyValuePairs.Select(kv => kv.Value).ToArray();
        }
    }
}