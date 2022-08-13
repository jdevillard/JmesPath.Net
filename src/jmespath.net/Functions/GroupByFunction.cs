using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevLab.JmesPath.Functions
{
    public class GroupByFunction : ByFunction
    {
        public GroupByFunction()
            : base("group_by")
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate(args);

            System.Diagnostics.Debug.Assert(args.Length == 2);
            System.Diagnostics.Debug.Assert(args[0].IsToken);
            System.Diagnostics.Debug.Assert(args[1].IsExpressionType);

            var array = (JArray)args[0].Token;
            if (array.Any(i => i.Type != JTokenType.Object))
                throw new Exception($"Error: invalid-type, function {Name} expects its first argument to be an array of objects.");
        }
        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(args.Length == 2);
            System.Diagnostics.Debug.Assert(args[0].IsToken);
            System.Diagnostics.Debug.Assert(args[1].IsExpressionType);

            var array = (JArray)args[0].Token;
            var expression = args[1].Expression;

            var dictionary = new Dictionary<string, IList<JToken>>();

            foreach (var element in array)
            {
                string key = "";

                var token = expression.Transform(element).AsJToken();
                if (token != JTokens.Null)
                {
                    if (token.GetTokenType() != "string")
                        continue;

                    key = token.Value<string>();
                    AddElement(dictionary, key, element);
                }
            }

            var properties = dictionary.Select(kvp => new JProperty(kvp.Key, kvp.Value));

            return new JObject(properties);
        }

        private static void AddElement(IDictionary<string, IList<JToken>> dictionary, string key, JToken element)
        {
            if (!dictionary.ContainsKey(key))
                dictionary[key] = new List<JToken>();

            dictionary[key].Add(element);
        }
    }
}