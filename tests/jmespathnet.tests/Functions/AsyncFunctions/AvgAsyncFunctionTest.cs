using System.Threading.Tasks;
using DevLab.JmesPath;
using jmespath.net.tests.Parser;

namespace jmespath.net.tests.Functions
{
    using FactAttribute = Xunit.FactAttribute;

    public class AvgAsyncFunctionTest : ParserTestBase
    {
        protected override void RegisterFunction(JmesPath parser)
        {
            base.RegisterFunction(parser);
            parser.FunctionRepository.Register<AvgAsyncFunction>();
        }

        [Fact]
        public async Task AsyncWithinSync()
        {
            const string json = "{\"foo\":[2, 3]}";
            const string expression = "floor(avgasync(foo))";
            const string expected = "2";

            await AssertAsync(expression, json, expected);
        }
        
        [Fact]
        public async Task SyncWithinAsync()
        {
            const string json = "{\"foo\":[2.5, 3.5]}";
            const string expression = "avgasync([floor(foo[0]), ceil(foo[1])])";
            const string expected = "3.0";

            await AssertAsync(expression, json, expected);
        }
        
        [Fact]
        public async Task AsyncWithPipe()
        {
            const string json = "{\"foo\":[2, 3]}";
            const string expression = "avgasync(foo) | floor(@)";
            const string expected = "2";

            await AssertAsync(expression, json, expected);
        }
    }
}