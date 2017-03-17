
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public struct JmesPathFunctionArgument
    {
        private readonly JToken token_;
        private readonly JmesPathExpression expression_;

        public JmesPathFunctionArgument(JToken token)
        {
            System.Diagnostics.Debug.Assert(token != null);

            token_ = token;
            expression_ = null;
        }

        public JmesPathFunctionArgument(JmesPathExpression expression)
        {
            System.Diagnostics.Debug.Assert(expression.IsExpressionType);

            token_ = null;
            expression_ = expression;
        }

        public bool IsExpressionType
            => expression_ != null;

        public bool IsToken
            => token_ != null;

        public JmesPathExpression Expression
            => expression_;

        public JToken Token
            => token_;
    }
}