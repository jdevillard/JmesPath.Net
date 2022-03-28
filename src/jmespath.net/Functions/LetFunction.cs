using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class LetFunction : JmesPathFunction
    {
        public LetFunction(IScopeParticipant scopes)
            : base("let", 2, scopes)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            System.Diagnostics.Debug.Assert(base.Scopes != null);

            EnsureObject(args[0]);
            EnsureExpressionType(args[1]);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            scopes_?.PushScope(args[0].Token);

            try
            {
                var expression = args[1].Expression;
                var result = expression.Transform(Context);

                return result.AsJToken();
            }
            finally
            {
                scopes_?.PopScope();
            }
        }
    }
}