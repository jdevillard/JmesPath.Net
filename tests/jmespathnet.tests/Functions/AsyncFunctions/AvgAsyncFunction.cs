using System.Linq;
using System.Threading.Tasks;
using DevLab.JmesPath.Functions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace jmespath.net.tests.Functions
{
    public class AvgAsyncFunction : JmesPathFunction
    {
        public AvgAsyncFunction()
            : base("avgasync", 1)
        {
        }

        public override void Validate(params JmesPathFunctionArgument[] args)
        {
            base.Validate();
            
            EnsureArrayOf(args[0], "number");
        }

        public override JToken Execute(params JmesPathFunctionArgument[] args)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<JToken> ExecuteAsync(params JmesPathFunctionArgument[] args)
        {
            // Simulate asynchronous behavior
            return await Task.Run(() =>
            {
                System.Diagnostics.Debug.Assert(args.Length == 1);
                System.Diagnostics.Debug.Assert(args[0].IsToken);

                var array = args[0].Token as JArray;
                var collection = array.Select(u => u.Value<double>());

                return collection.Any()
                        ? new JValue(collection.Average())
                        : JTokens.Null
                    ;
            });
        }
    }
}