using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public abstract class JmesPathOrderingComparison : JmesPathComparison
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathOrderingComparison" /> class
        /// that performs a comparison between two specified expressions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        protected JmesPathOrderingComparison(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        {
        }

        protected static bool FromDouble(JToken token, out double? value)
        {
            value = null;

            if (
                token.Type != JTokenType.Float &&
                token.Type != JTokenType.Integer
            )
                return false;

            value = token.Value<double>();
            return true;
        }

        protected static bool FromString(JToken token, out string value)
        {
            value = null;

            if (
                token.Type != JTokenType.Bytes &&
                token.Type != JTokenType.Date &&
                token.Type != JTokenType.Guid &&
                token.Type != JTokenType.String &&
                token.Type != JTokenType.TimeSpan &&
                token.Type != JTokenType.Uri &&

                // null is considered a valid string

                token.Type != JTokenType.Null
            )
                return false;

            value = token.Value<string>();
            return true;
        }

        protected override bool? Compare(JToken left, JToken right)
        {
            // originally, comparisons was only specified for integers and floating point numbers.
            // however, a change in the python implementation meant that other implicit comparisons
            // stopped working. Because this impacts a lot of existing code, comparison for strings
            // is now legal.

            // see : https://github.com/jmespath/jmespath.py/issues/124

            if (FromDouble(left, out var lhd) && FromDouble(right, out var rhd))
            {
                if (lhd == null)
                    return null;
                if (rhd == null)
                    return null;

                return Compare(lhd.Value, rhd.Value);
            }

            else if (FromString(left, out var lhs) && FromString(right, out var rhs))
            {
                return Compare(lhs, rhs);
            }

            // other comparisons are currently not supported
            // likewise, comparisons between values from different types are not supported.

            return null;
        }

        protected abstract bool Compare(double left, double right);
        protected abstract bool Compare(string left, string right);
    }
}