using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Interop;
using DevLab.JmesPath.Utils;
using System.Collections.Generic;
using System;

namespace DevLab.JmesPath
{
    public sealed class ScopeParticipant : IScopeParticipant, IContextEvaluator
    {
        private readonly Stack<JToken> scopes_
            = new Stack<JToken>()
            ;

        private JToken root_ = JTokens.Null;

        JToken IContextEvaluator.Root => root_;

        public JToken Evaluate(string identifier)
        {
            if (scopes_.Count == 0)
                throw new Exception($"Error: undefined-variable, the variable '${identifier}' is not defined.");

            foreach (var scope in scopes_)
            {
                if (scope[identifier] != null)
                    return scope[identifier];
            }

            throw new Exception($"Error: undefined-variable, the variable '${identifier}' is not defined.");
        }

        public void SetRoot(JToken root) {
            root_ = root;
        }
        public void PushScope(JToken token)
        {
            scopes_.Push(token);
        }
        public void PopScope()
        {
            scopes_.Pop();
        }
    }
}
