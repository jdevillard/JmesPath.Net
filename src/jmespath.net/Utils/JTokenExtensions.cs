using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Utils
{
    public static class JTokenExtensions
    {
        /// <summary>
        /// Returns the string representation of the specified JSON token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string AsString(this JToken token)
        {
            var builder = new StringBuilder();
            using (var textWriter = new StringWriter(builder))
            using (var jsonWriter = new JsonTextWriter(textWriter))
                token.WriteTo(jsonWriter);

            return builder.ToString();
        }

        /// <summary>
        /// Returns the string representation of the specified token type.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetTokenType(this JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    return "object";

                case JTokenType.Array:
                    return "array";

                case JTokenType.Integer:
                case JTokenType.Float:
                    return "number";

                case JTokenType.Boolean:
                    return "boolean";

                case JTokenType.Null:
                    return "null";

                case JTokenType.Bytes:
                case JTokenType.Date:
                case JTokenType.Guid:
                case JTokenType.String:
                case JTokenType.TimeSpan:
                case JTokenType.Uri:
                    return "string";

                default:
                    System.Diagnostics.Debug.Assert(false);
                    throw new NotSupportedException();
            }
        }
    }
}