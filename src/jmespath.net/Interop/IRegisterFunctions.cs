using DevLab.JmesPath.Expressions;

namespace DevLab.JmesPath.Interop
{
    public interface IRegisterFunctions
    {
        IRegisterFunctions Register(string name, JmesPathFunction function);
        IRegisterFunctions Register<T>() where T : JmesPathFunction;
    }
}