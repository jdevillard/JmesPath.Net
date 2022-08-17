using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathSubtractionExpression : JmesPathArithmeticExpression
    {
        public JmesPathSubtractionExpression(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        { }
        protected override double Compute(double left, double right)
            => left - right;
    }
    public sealed class JmesPathUnaryMinusExpression : JmesPathSubtractionExpression
    {
        public JmesPathUnaryMinusExpression(JmesPathExpression expression)
            : base(new JmesPathLiteral(JToken.Parse("0")), expression)
        {}
    }
}