using System.Collections.Generic;
using DevLab.JmesPath.Functions;

namespace DevLab.JmesPath.Interop
{
    public interface IFunctionRepository
    {
        IEnumerable<string> Names { get; }
        JmesPathFunction this[string name] { get; }
        bool Contains(string name);
    }
}