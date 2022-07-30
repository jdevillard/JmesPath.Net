namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathSubtractionExpression : JmesPathArithmeticExpression
    {
        public JmesPathSubtractionExpression(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        { }
        protected override double Compute(double left, double right)
            => left - right;
    }
}