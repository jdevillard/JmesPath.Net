using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathIndexExpression : JmesPathCompoundExpression
    {
        public JmesPathIndexExpression(JmesPathExpression expression, JmesPathExpression specifier)
            : base(expression, specifier)
        {
            System.Diagnostics.Debug.Assert(
                specifier is JmesPathIndex ||
                specifier is JmesPathSliceProjection ||
                specifier is JmesPathFlattenProjection ||
                specifier is JmesPathListWildcardProjection ||
                false
                );
        }
    }
}