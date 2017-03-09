using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathNotEqualOperator : JmesPathComparison
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathNotEqualOperator"/> class.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public JmesPathNotEqualOperator(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        {

        }

        protected override bool? Compare(JToken left, JToken right)
        {
            return !JToken.DeepEquals(left, right);
        }
    }
}