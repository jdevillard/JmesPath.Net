using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Utils;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathArgument
    {
        public static JToken JNull = JValue.Parse("null");
        public static JmesPathArgument Null = new JmesPathArgument(JNull);

        public JmesPathArgument(JToken token)
        {
            Token = token ?? JNull;
        }

        public JmesPathArgument(IEnumerable<JmesPathArgument> projection)
        {
            System.Diagnostics.Debug.Assert(projection != null);
            Projection = projection.ToArray();
        }

        public bool IsProjection
            => Projection != null
            ;

        public static implicit operator JmesPathArgument(JToken token)
        {
            return new JmesPathArgument(token);
        }

        public JToken Token { get; internal set; }
        public JmesPathArgument[] Projection { get; internal set; }

        public JToken AsJToken()
        {
            if (Token != null)
                return Token;

            var items = new List<JToken>();
            foreach (var projected in Projection)
                items.Add(projected.AsJToken());

            return new JArray().AddRange(items);
        }

        public override string ToString()
        {
            if (Token != null)
                return $"T:<{Token}>";
            else
            {
                var builder = new StringBuilder();
                foreach (var argument in Projection)
                    builder.Append(argument);
                return $"P:<{builder.ToString()}>";
            }
        }
    }
}