using DevLab.JmesPath.Interop;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathIdentifier : JmesPathExpression
    {
        private readonly string name_;
        internal IContextEvaluator evaluator_;

        public JmesPathIdentifier(string name)
        {
            name_ = name;
        }

        public string Name => name_;

        protected override JmesPathArgument Transform(JToken json)
        {
            var jsonObject = json as JObject;
            return jsonObject?[name_] ?? Evaluate(name_);
        }

        public override string ToString()
        {
            return $"JmesPathIdentifier: {name_}";
        }

        public JToken Evaluate(string identifier)
            => evaluator_?.Evaluate(identifier) ?? JTokens.Null;
    }
}