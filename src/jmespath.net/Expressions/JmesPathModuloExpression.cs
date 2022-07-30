namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathModuloExpression : JmesPathArithmeticExpression
    {
        public JmesPathModuloExpression(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        { }
        protected override double Compute(double left, double right)
            => left % right;
    }
}