using System;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public abstract class JmesPathAsyncFunction : JmesPathFunction
    {
        protected JmesPathAsyncFunction(string name, int count) 
            : base(name, count, null, false, null)
        { }
        protected JmesPathAsyncFunction(string name, int count, IScopeParticipant scopes) 
            : base(name, count, null, false, scopes)
        { }

        protected JmesPathAsyncFunction(string name, int minCount, bool variadic)
            : base(name, minCount, null, variadic, null)
        { }

        protected JmesPathAsyncFunction(string name, int minCount, int maxCount)
            : base(name, minCount, maxCount, false, null)
        { }

        protected JmesPathAsyncFunction(string name, int minCount, int? maxCount, bool variadic, IScopeParticipant scopes) 
            : base(name, minCount, maxCount, variadic, scopes)
        { }

        public sealed override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            throw new InvalidOperationException("This function is async. Use ExecuteAsync instead.");
        }
    }
}