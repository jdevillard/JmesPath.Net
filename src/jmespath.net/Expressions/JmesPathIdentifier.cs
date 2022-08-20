using DevLab.JmesPath.Interop;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathIdentifier : JmesPathExpression, IContextHolder
    {
        private readonly string name_;

        public JmesPathIdentifier(string name)
        {
            name_ = name;
        }

        public string Name => name_;

        IContextEvaluator IContextHolder.Evaluator { get; set ; }

        protected override JmesPathArgument Transform(JToken json)
        {
            var jsonObject = json as JObject;
            return jsonObject?[name_] ?? Evaluate(name_);
        }

        protected override string Format()
            => StringUtil.WrapIdentifier(name_);

        public JToken Evaluate(string identifier)
            => (this as IContextHolder).Evaluator?.Evaluate(identifier) ?? JTokens.Null;
    }
}