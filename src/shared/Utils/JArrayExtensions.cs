using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace DevLab.JmesPath.Utils
{
    public static class JArrayExtensions
    {
        /// <summary>
        /// Populates a JSON array with a list of JSON tokens.
        /// This extension method is an helpful replacement
        /// for the JArray ctor which takes a object[] parameter.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public static JArray AddRange(this JArray array, params JToken[] tokens)
        {
            if (tokens == null)
                return array;

            foreach (var token in tokens)
                array.Add(token);

            return array;
        }

        public static JArray AddRange(this JArray array, IEnumerable<JToken> tokens)
        {
            return AddRange(array, tokens.ToArray());
        }
    }
}