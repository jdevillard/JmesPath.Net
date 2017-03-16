using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class NotNullFunction : JmesPathFunction
    {
        public NotNullFunction()
            : base("not_null", 1,true)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            foreach (var jmesPathArgument in args)
            {
                var token = jmesPathArgument.AsJToken();
                if (token.Type != JTokenType.Null)
                    return token;
            }

            return null;
        }
    }
}