namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathMultiplicationExpression : JmesPathArithmeticExpression
    {
        public JmesPathMultiplicationExpression(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        { }
        protected override double Compute(double left, double right)
            => left * right;
    }
}