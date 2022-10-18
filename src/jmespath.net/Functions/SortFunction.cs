using System;
using System.Collections.Generic;
using System.Linq;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class SortFunction : JmesPathFunction
    {
        public SortFunction()
            : base("sort", 1)
        {

        }
        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            EnsureArrayOfSame(args[0]);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var array = (JArray)args[0].Token;

            if (array.Count == 0)
                return new JArray();

            var item = array[0];

            if (item.Type == JTokenType.Float)
                return JArray.FromObject(SortNumber<double>(array));
            else if (item.Type == JTokenType.Integer)
                return JArray.FromObject(SortNumber<int>(array));
            else
                return JArray.FromObject(SortText(array));
        }

        internal static T[] SortNumber<T>(JArray array)
            => array
                .Values<T>()
                .OrderBy(u => u)
                .ToArray()
            ;

        internal static string[] SortText(JArray array)
            => array
                .Select(u => (Text)u.Value<string>())
                .OrderBy(u => u, Text.CodePointComparer)
                .Select(u => (string)u)
                .ToArray()
                ;
    }
}