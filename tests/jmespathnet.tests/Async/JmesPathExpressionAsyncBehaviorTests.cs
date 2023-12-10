using System.Threading.Tasks;
using DevLab.JmesPath;
using jmespathnet.tests.Async.SampleFunctions;
using Xunit;

namespace jmespathnet.tests.Async
{
    public class JmesPathExpressionAsyncBehaviorTests
    {
        private readonly JmesPath jmesPath;
            
        public JmesPathExpressionAsyncBehaviorTests()
        {
            jmesPath = new JmesPath();
            jmesPath.FunctionRepository.Register<AsyncAvgFunction>();
        }
        
        [Fact]
        public async Task SimpleExpression()
        {
            const string input = "{\"foo\":[2, 3]}";
            const string expression = "floor(asyncavg(foo))";
            const string expected = "2";

            var result = await jmesPath.TransformAsync(input, expression);
            Assert.Equal(expected, result);
        }
        

        [Fact]
        public async Task AsyncWithinSync()
        {
            const string input = "{\"foo\":[2, 3]}";
            const string expression = "floor(asyncavg(foo))";
            const string expected = "2";

            var result = await jmesPath.TransformAsync(input, expression);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task SyncWithinAsync()
        {
            const string input = "{\"foo\":[2.5, 3.5]}";
            const string expression = "asyncavg([floor(foo[0]), ceil(foo[1])])";
            const string expected = "3.0";

            var result = await jmesPath.TransformAsync(input, expression);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task AsyncWithPipe()
        {
            const string input = "{\"foo\":[2, 3]}";
            const string expression = "asyncavg(foo) | floor(@)";
            const string expected = "2";

            var result = await jmesPath.TransformAsync(input, expression);
            Assert.Equal(expected, result);
        }
        
        
    }
}