using System.Threading.Tasks;
using DevLab.JmesPath;
using jmespathnet.tests.Async.SampleFunctions;
using Xunit;

namespace jmespathnet.tests.Async
{
    public class ComparisonAsyncBehaviorTests
    {
        private readonly JmesPath jmesPath =  new JmesPath();

        [Fact]
        public async Task AsyncWithEqual()
        {
            const string input = "{\"foo\": 2.5}";
            const string expression = "foo == `2.5`";
            const string expected = "true";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithNotEqual()
        {
            const string input = "{\"foo\": 2.5}";
            const string expression = "foo != `2`";
            const string expected = "true";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithGreaterThan()
        {
            const string input = "{\"foo\": 2.5}";
            const string expression = "foo > `2`";
            const string expected = "true";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithGreaterThanOrEqual()
        {
            const string input = "{\"foo\": 2.5}";
            const string expression = "foo >= `2.5`";
            const string expected = "true";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithLessThan()
        {
            const string input = "{\"foo\": 2.5}";
            const string expression = "foo < `3`";
            const string expected = "true";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithLessThanOrEqual()
        {
            const string input = "{\"foo\": 2.5}";
            const string expression = "foo <= `2.5`";
            const string expected = "true";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
    }
}