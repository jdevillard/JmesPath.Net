using System.Threading.Tasks;
using DevLab.JmesPath;
using Xunit;

namespace jmespathnet.tests.Async
{
    public class ArithmeticExpressionsAsyncBehaviorTests
    {
        private readonly JmesPath jmesPath= new JmesPath();
            
        [Fact]
        public async Task AsyncWithAdd()
        {
            const string input = "{\"foo\": 40}";
            const string expression = "foo + `2`";
            const string expected = "42.0";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithSubtract()
        {
            const string input = "{\"foo\": 44}";
            const string expression = "foo - `2`";
            const string expected = "42.0";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithMultiply()
        {
            const string input = "{\"foo\": 21}";
            const string expression = "foo * `2`";
            const string expected = "42.0";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithDivision()
        {
            const string input = "{\"foo\": 84}";
            const string expression = "foo / `2`";
            const string expected = "42.0";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
    }
}