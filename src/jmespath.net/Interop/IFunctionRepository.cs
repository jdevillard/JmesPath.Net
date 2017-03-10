using System.Collections.Generic;
using DevLab.JmesPath.Expressions;

namespace DevLab.JmesPath.Interop
{
    public interface IFunctionRepository
    {
        IEnumerable<string> Names { get; }
        JFunction this[string name] { get; }
        bool Contains(string name);
    }
}