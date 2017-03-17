using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class MergeFunction : JmesPathFunction
    {
        public MergeFunction()
            : base("merge", 1, true)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            EnsureObject(args[0]);
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            var result = new JObject();

            foreach (var argument in args)
            {
                var token = (JObject)argument.Token;
                result.Merge(token);
            }

            return result;
        }
    }
}