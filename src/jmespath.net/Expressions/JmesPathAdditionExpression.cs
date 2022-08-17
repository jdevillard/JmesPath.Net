using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathAdditionExpression : JmesPathArithmeticExpression
    {
        public JmesPathAdditionExpression(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        { }
        protected override double Compute(double left, double right)
            => left + right;
    }
    public sealed class JmesPathUnaryPlusExpression : JmesPathAdditionExpression
    {
        public JmesPathUnaryPlusExpression(JmesPathExpression expression)
            : base(new JmesPathLiteral(JToken.Parse("0")), expression)
        {}
    }
}