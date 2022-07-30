namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathAdditionExpression : JmesPathArithmeticExpression
    {
        public JmesPathAdditionExpression(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        { }
        protected override double Compute(double left, double right)
            => left + right;
    }
}