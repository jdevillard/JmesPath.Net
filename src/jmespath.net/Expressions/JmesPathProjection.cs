using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    public abstract class JmesPathProjection : JmesPathExpression
    {
        protected abstract JmesPathArgument Project(JmesPathArgument argument);

        protected virtual Task<JmesPathArgument> ProjectAsync(JmesPathArgument argument) =>
            Task.FromResult(Project(argument));

       protected override JmesPathArgument Transform(JToken json)
            => Project(json);

       protected override Task<JmesPathArgument> TransformAsync(JToken json) => ProjectAsync(json);
    }
}