using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public abstract class JmesPathProjection : JmesPathExpression
    {
        protected abstract JmesPathArgument Project(JmesPathArgument argument);

        protected override JmesPathArgument Transform(JToken json)
            => Project(json);
    }
}