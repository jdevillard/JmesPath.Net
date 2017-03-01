using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathIndexExpression : JmesPathExpression
    {
        private readonly JmesPathExpression expression_;
        private readonly JmesPathExpression specifier_;

        public JmesPathIndexExpression(JmesPathExpression expression, JmesPathNumber index)
            : this(expression, new JmesPathBracketSpecifier(index))
        {
        }

        public JmesPathIndexExpression(JmesPathExpression expression, JmesPathExpression specifier)
        {
            expression_ = expression;
            specifier_ = specifier;
        }

        public override JToken Transform(JToken json)
        {
            var token = expression_.Transform(json);
            return token == null ? null : specifier_.Transform(token);
        }
    }
}