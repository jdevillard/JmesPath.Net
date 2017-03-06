using System;

namespace DevLab.JmesPath.Expressions
{
    /// <summary>
    /// Represents a JmesPath sub expression.
    /// </summary>
    public sealed class JmesPathSubExpression : JmesPathCompoundExpression
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathSubExpression"/> class
        /// with two <see cref="JmesPathExpression"/> objects.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="subExpression"></param>
        public JmesPathSubExpression(JmesPathExpression expression, JmesPathExpression subExpression)
            : base(expression, subExpression)
        {
            System.Diagnostics.Debug.Assert(
                subExpression is JmesPathIdentifier ||
                subExpression is JmesPathMultiSelectHash ||
                subExpression is JmesPathMultiSelectList ||
                subExpression is JmesPathHashWildcardProjection ||
                false
                );
        }
    }
}