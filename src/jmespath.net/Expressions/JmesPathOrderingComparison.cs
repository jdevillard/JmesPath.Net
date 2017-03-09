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

        protected static double? Evaluate(JToken token)
        {
            if (token.Type != JTokenType.Float && token.Type != JTokenType.Integer)
                return null;

            return token.Value<double>();
        }

        protected override bool? Compare(JToken left, JToken right)
        {
            var lhs = Evaluate(left);
            var rhs = Evaluate(right);

            if (lhs == null)
                return null;
            if (rhs == null)
                return null;

            return Compare(lhs.Value, rhs.Value);
        }

        protected abstract bool Compare(double left, double right);
    }
}