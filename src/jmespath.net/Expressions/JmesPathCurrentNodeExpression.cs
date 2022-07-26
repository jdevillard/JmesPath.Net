using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public class JmesPathCurrentNodeExpression : JmesPathExpression
    {
        protected override JmesPathArgument Transform(JToken json)
            => json;

        protected override string Format()
            => "@";
    }
}