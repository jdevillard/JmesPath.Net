using DevLab.JmesPath.Expressions;

namespace DevLab.JmesPath.Interop
{
    public interface IRegisterFunctions
    {
        IRegisterFunctions Register(string name, JFunction function);
        IRegisterFunctions Register<T>() where T : JFunction;
    }
}