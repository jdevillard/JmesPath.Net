using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Utils;

namespace DevLab.JmesPath.Expressions
{
    public struct JmesPathArgument : IEquatable<JmesPathArgument>
    {
        public static JmesPathArgument Null = new JmesPathArgument(JTokens.Null);
        public static JmesPathArgument True = new JmesPathArgument(JTokens.True);
        public static JmesPathArgument False = new JmesPathArgument(JTokens.False);

        public JmesPathArgument(JToken token)
        {
            Token = token ?? JTokens.Null;
            Projection = null;
        }

        public JmesPathArgument(IEnumerable<JmesPathArgument> projection)
        {
            Token = null;
            Debug.Assert(projection != null);
            Projection = projection.ToArray();
        }

        public bool IsProjection
            => Projection != null
            ;

        public static implicit operator JmesPathArgument(JToken token)
        {
            return new JmesPathArgument(token);
        }

        public JToken Token { get; }

        public JmesPathArgument[] Projection { get; }

        public JToken AsJToken()
        {
            if (Token != null)
                return Token;

            var items = new List<JToken>();
            foreach (var projected in Projection)
                items.Add(projected.AsJToken());

            return new JArray().AddRange(items);
        }

        public static bool IsFalse(JmesPathArgument argument)
        {
            var token = argument.AsJToken();
            return JTokens.IsFalse(token);
        }

        public bool Equals(JmesPathArgument other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is JmesPathArgument))
                return false;
            return Equals((JmesPathArgument)obj);
        }

        public static bool operator ==(JmesPathArgument left, JmesPathArgument right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(JmesPathArgument left, JmesPathArgument right)
        {
            return !left.Equals(right);
        }
        public override int GetHashCode()
        {
            const int seedPrimeNumber = 691;
            const int fieldPrimeNumber = 397;

            var hashCode = seedPrimeNumber;

            unchecked
            {
                if (Token != null)
                    hashCode *= fieldPrimeNumber + Token.GetHashCode();
                else
                {
                    // a projection does not contain null values

                    foreach (var item in Projection)
                        hashCode *= fieldPrimeNumber + item.GetHashCode();
                }
            }

            return hashCode;
        }

#if DEBUG
        public override string ToString()
        {
            if (Token != null)
                return $"T:<{Token}>";
            else
            {
                var builder = new StringBuilder();
                foreach (var argument in Projection)
                    builder.Append(argument);
                return $"P:<{builder}>";
            }
        }
#endif
    }
}