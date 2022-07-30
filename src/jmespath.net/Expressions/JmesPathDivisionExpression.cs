namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathDivisionExpression : JmesPathArithmeticExpression
    {
        public JmesPathDivisionExpression(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        { }
        protected override double Compute(double left, double right)
            => left / right;
    }
}