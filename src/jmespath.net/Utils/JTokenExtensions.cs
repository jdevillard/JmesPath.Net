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
    }
}