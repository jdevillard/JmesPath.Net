using System;

namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathLessThanOrEqualOperator : JmesPathOrderingComparison
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathLessThanOrEqualOperator"/> class
        /// to perform a comparison between two specified expressions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public JmesPathLessThanOrEqualOperator(JmesPathExpression left, JmesPathExpression right)
            : base(left, right, "<=")
        {
        }

        protected override bool Compare(double left, double right)
            => left <= right;

        protected override bool Compare(string left, string right)
            => (left == null) ? true
                : string.Compare(left, right, StringComparison.Ordinal) <= 0
                ;
    }
}