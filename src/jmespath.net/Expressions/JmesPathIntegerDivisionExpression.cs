namespace DevLab.JmesPath.Expressions
{
    public sealed class JmesPathIntegerDivisionExpression : JmesPathArithmeticExpression
    {
        public JmesPathIntegerDivisionExpression(JmesPathExpression left, JmesPathExpression right)
            : base(left, right)
        { }
        protected override double Compute(double left, double right)
            => (int) (left / right);
    }
}