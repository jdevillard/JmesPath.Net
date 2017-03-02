using System.Collections.Generic;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathArgument
    {
        public JmesPathArgument(JToken token)
            : this(token, false)
        {
        }

        public JmesPathArgument(IEnumerable<JToken> tokens, bool isProjection)
            : this(new JArray().AddRange(tokens), isProjection)
        {
        }

        internal JmesPathArgument(JToken token, bool isProjection)
        {
            Token = token;
            IsProjection = isProjection;
        }

        public JTokenType Type
            => Token.Type
            ;

        public bool IsProjection { get; private set; }

        public static implicit operator JmesPathArgument(JToken token)
        {
            return new JmesPathArgument(token);
        }

        public JToken Token { get; internal set; }

        public override string ToString()
        {
            return $"{Token} {(IsProjection ? "(Projection)" : "")}";
        }
    }
}