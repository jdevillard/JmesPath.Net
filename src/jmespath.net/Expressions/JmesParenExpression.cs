namespace DevLab.JmesPath.Expressions
{
    public class JmesParenExpression : JmesPathSimpleExpression
    {
        public JmesParenExpression(JmesPathExpression expression)
            : base(expression)
        {
        }

        public override string ToString()
            => $"({Expression})";
    }
}