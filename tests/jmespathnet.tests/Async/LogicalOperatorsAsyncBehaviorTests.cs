using System.Threading.Tasks;
using DevLab.JmesPath;
using Xunit;

namespace jmespathnet.tests.Async
{
    public class LogicalOperatorsAsyncBehaviorTests
    {
        private readonly JmesPath jmesPath = new JmesPath();
            
        [Fact]
        public async Task AsyncWithAnd()
        {
            const string input = "{\"foo\":false, \"bar\":true}";
            const string expression = "!foo && bar";
            const string expected = "true";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithOr()
        {
            const string input = "{\"foo\":false, \"bar\":true}";
            const string expression = "foo || bar";
            const string expected = "true";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithNot()
        {
            const string input = "{\"foo\":true}";
            const string expression = "!foo";
            const string expected = "false";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
    }
}