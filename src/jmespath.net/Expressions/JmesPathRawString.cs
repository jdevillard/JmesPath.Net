using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathRawString : JmesPathExpression
    {
        private readonly string value_;

        public JmesPathRawString(string value)
        {
            value_ = value;
        }

        public string Value => value_;

        public override JToken Transform(JToken json)
        {
            return new JValue(Value);
        }
    }
}