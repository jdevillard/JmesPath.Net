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

        public override JToken Transform(JToken json)
        {
            System.Diagnostics.Debug.Assert(json.Type == JTokenType.Object);
            var jsonObject = json as JObject;
            System.Diagnostics.Debug.Assert(jsonObject != null);

            return jsonObject[name_];
        }
    }
}