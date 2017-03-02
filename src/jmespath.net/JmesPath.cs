using System;
using System.IO;
using System.Text;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath
{
    public sealed class JmesPath
    {
        public static JToken JNull = JValue.Parse("null");

        public String Transform(string json, string expression)
        {
            var jmesPath = Parse(expression);
            if (jmesPath == null)
                return null;

            var token = JToken.Parse(json);
            var result = jmesPath.Transform(token);

            return (result?.Token ?? JNull).AsString();
        }

        private static JmesPathExpression Parse(string expression)
        {
            return Parse(new MemoryStream(Encoding.UTF8.GetBytes(expression)));
        }

        private static JmesPathExpression Parse(Stream stream)
        {
            var scanner = new JmesPathScanner(stream);
            var analyzer = new JmesPathParser(scanner);
            if (!analyzer.Parse())
                return null;

            return analyzer.Expression;
        }
    }
}
