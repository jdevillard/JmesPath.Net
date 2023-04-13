using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathIdentifier : JmesPathExpression
    {
        private readonly string name_;

        public JmesPathIdentifier(string name)
        {
            name_ = name;
        }

        public string Name => name_;

        protected override JmesPathArgument Transform(JToken json)
            => (json as JObject)?[name_] ?? JTokens.Null;

        protected override string Format()
            => StringUtil.WrapIdentifier(name_);
    }
}