using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathIndexExpression : JmesPathCompoundExpression
    {
        public JmesPathIndexExpression(JmesPathExpression expression, JmesPathExpression specifier)
            : base(expression, specifier)
        {
            System.Diagnostics.Debug.Assert(
                specifier is JmesPathFilterExpression ||
                specifier is JmesPathIndex ||
                specifier is JmesPathFlattenProjection ||
                specifier is JmesPathListWildcardProjection ||
                specifier is JmesPathSliceProjection ||
                false
                );
        }
    }
}