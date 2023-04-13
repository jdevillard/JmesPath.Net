using DevLab.JmesPath.Interop;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathVariableReference : JmesPathExpression, IContextHolder
    {
        private readonly string name_;
        private IContextEvaluator evaluator;

        public JmesPathVariableReference(string name)
        {
            System.Diagnostics.Debug.Assert(name.StartsWith("$"));
            name_ = name.Substring(1);
        }

        public string Name => name_;

        public IContextEvaluator Evaluator { get; set; }

        public override void Accept(IVisitor visitor)
        {
            base.Accept(visitor);
        }

        protected override JmesPathArgument Transform(JToken json)
        {
            var result = Evaluator.Evaluate(name_);
            return result;
        }

        protected override string Format()
            => $"${StringUtil.WrapIdentifier(name_)}";
    }
}