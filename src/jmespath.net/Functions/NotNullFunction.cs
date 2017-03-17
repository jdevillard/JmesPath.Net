using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class NotNullFunction : JmesPathFunction
    {
        public NotNullFunction()
            : base("not_null", 1, true)
        {

        }
        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            foreach (var jmesPathArgument in args)
            {
                var token = jmesPathArgument.Token;
                if (token.Type != JTokenType.Null)
                    return token;
            }

            return JTokens.Null;
        }
    }
}