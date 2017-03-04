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

        protected override JToken Transform(JToken json)
        {
            var jsonObject = json as JObject;
            return jsonObject?[name_];
        }
    }
}