using System;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Interop
{
    public abstract class JmesPathFunction
    {
        protected JmesPathFunction(string name, int count)
            : this(name, count, false)
        {
        }

        protected JmesPathFunction(string name, int minCount, bool variadic)
        {
            Name = name;
            MinArgumentCount = minCount;
            Variadic = variadic;
        }

        public string Name { get; private set; }

        public int MinArgumentCount { get; }

        public bool Variadic { get; }

        public abstract bool Validate(params JmesPathArgument[] args);
        public abstract JToken Execute(params JmesPathArgument[] args);
    }
}