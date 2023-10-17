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
        public async Task Avg()
        {
            const string json = "{\"foo\":[2, 3]}";
            const string expression = "floor(avg(foo))";
            const string expected = "2";

            await AssertAsync(expression, json, expected);
        }
    }
}