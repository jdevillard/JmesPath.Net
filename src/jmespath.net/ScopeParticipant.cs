using Newtonsoft.Json.Linq;
using DevLab.JmesPath.Interop;
using DevLab.JmesPath.Utils;
using System.Collections.Generic;

namespace DevLab.JmesPath
{
    public sealed class ScopeParticipant : IScopeParticipant, IContextEvaluator
    {
        private readonly Stack<JToken> scopes_
            = new Stack<JToken>()
            ;
        public JToken Evaluate(string identifier)
        {
            if (scopes_.Count == 0)
                return JTokens.Null;

            foreach (var scope in scopes_)
            {
                if (scope[identifier] != null)
                    return scope[identifier];
            }

            return JTokens.Null;
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
