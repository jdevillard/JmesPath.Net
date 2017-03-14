using DevLab.JmesPath.Expressions;

namespace DevLab.JmesPath.Interop
{
    public interface IVisitor
    {
        void Visit(JmesPathExpression expression);
    }
}