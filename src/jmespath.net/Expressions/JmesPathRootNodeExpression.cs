using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathRootNodeExpression : JmesPathExpression, IContextHolder
    {
        IContextEvaluator IContextHolder.Evaluator { get; set; }

        protected override JmesPathArgument Transform(JToken json)
            => (this as IContextHolder).Evaluator?.Root;

        protected override string Format()
            => "$";
    }
}