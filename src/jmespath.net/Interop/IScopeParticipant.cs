using DevLab.JmesPath.Functions;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Interop
{
    /// <summary>
    /// The <see cref="IScopeParticipant" /> interface lets
    /// implementations participate in a stack of contexts
    /// to assist evaluating expressions.
    ///
    /// This supports the <see cref="LetFunction" />.
    /// </summary>
    public interface IScopeParticipant
    {
        void PushScope(JToken scope);
        void PopScope();
    }

    public interface IContextEvaluator
    {
        JToken Evaluate(string identifier);
    }

    public interface IContextHolder
    {
        IContextEvaluator Evaluator { get; set; }
    }
}