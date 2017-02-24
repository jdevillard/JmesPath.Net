using System;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    /// <summary>
    /// Represents a JmesPath sub expression.
    /// </summary>
    public sealed class JmesPathSubExpression : JmesPathExpression
    {
        private readonly JmesPathExpression expression_;
        private readonly JmesPathExpression subExpression_;

        /// <summary>
        /// Initialize a new instance of the <see cref="JmesPathSubExpression"/> class
        /// with two <see cref="JmesPathExpression"/> objects.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="subExpression"></param>
        public JmesPathSubExpression(JmesPathExpression expression, JmesPathExpression subExpression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (subExpression == null) throw new ArgumentNullException(nameof(subExpression));

            System.Diagnostics.Debug.Assert(subExpression is JmesPathIdentifier);

            expression_ = expression;
            subExpression_ = subExpression;
        }

        public override JToken Transform(JToken json)
        {
            var result = expression_.Transform(json);
            if (result == null)
                return null;
            return subExpression_.Transform(result);
        }
    }
}