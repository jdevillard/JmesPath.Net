namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathGreaterThanOrEqualOperator : JmesPathOrderingComparison
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathGreaterThanOrEqualOperator"/> class
        /// to perform a comparison between two specified expressions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public JmesPathGreaterThanOrEqualOperator(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        {
        }

        protected override bool Compare(double left, double right)
        {
            return left >= right;
        }
    }
}