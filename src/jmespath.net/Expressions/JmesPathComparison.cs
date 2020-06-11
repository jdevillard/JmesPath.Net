using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public abstract class JmesPathComparison : JmesPathCompoundExpression
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathComparison" /> class
        /// that performs a comparison between two specified expressions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        protected JmesPathComparison(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        {
        }

        protected abstract bool? Compare(JToken left, JToken right);

        protected override JmesPathArgument Transform(JToken json)
        {
            var left = Left.Transform(json).AsJToken();
            var right = Right.Transform(json).AsJToken();

            var result = Compare(left, right);

            return result == null
                ? JmesPathArgument.Null
                : result.Value ?
                    JmesPathArgument.True
                    : JmesPathArgument.False
                ;
        }
    }
}