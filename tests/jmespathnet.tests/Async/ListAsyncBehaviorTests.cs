using System.Threading.Tasks;
using DevLab.JmesPath;
using jmespathnet.tests.Async.SampleFunctions;
using Xunit;

namespace jmespathnet.tests.Async
{
    public class ListAsyncBehaviorTests
    {
        private readonly JmesPath jmesPath;

        public ListAsyncBehaviorTests()
        {
            jmesPath = new JmesPath();
            jmesPath.FunctionRepository.Register<AsyncAvgFunction>();
        }

        [Fact]
        public async Task AsyncWithListProjection()
        {
            const string input = "[{\"foo\":[2, 3]}, {\"foo\":[3, 4]}]";
            const string syncExpression = "[*].{avg: avg(foo)}.avg";
            const string asyncExpression = "[*].{avg: asyncavg(foo)}.avg";
            const string expected = "[2.5,3.5]";

            var syncResult = jmesPath.Transform(input, syncExpression);
            var asyncResult = await jmesPath.TransformAsync(input, asyncExpression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithSliceProjection()
        {
            const string input = "[{\"foo\":[2, 3]}, {\"foo\":[3, 4]}, {\"foo\":[4, 5]}]";
            const string syncExpression = "[1:].{avg: avg(foo)}.avg";
            const string asyncExpression = "[1:].{avg: asyncavg(foo)}.avg";
            const string expected = "[3.5,4.5]";

            var syncResult = jmesPath.Transform(input, syncExpression);
            var asyncResult = await jmesPath.TransformAsync(input, asyncExpression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithFilterAndFlattenProjection()
        {
            const string input = "[{\"foo\":[2, 3]}, {\"foo\":[3, 4]}]";
            const string syncExpression = "[?avg(foo) > `3`].foo[]";
            const string asyncExpression = "[?asyncavg(foo) > `3`].foo[]";
            const string expected = "[3,4]";

            var syncResult = jmesPath.Transform(input, syncExpression);
            var asyncResult = await jmesPath.TransformAsync(input, asyncExpression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithMultiselectList()
        {
            const string input = "[{\"foo\":1}, {\"foo\":2}]";
            const string expression = "[*].[foo]";
            const string expected = "[[1],[2]]";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithMultiselectHash()
        {
            const string input = "[{\"foo\":1}, {\"foo\":2}]";
            const string expression = "[*].{Count: foo}";
            const string expected = "[{\"Count\":1},{\"Count\":2}]";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithLetExpression()
        {
            const string input = "{\"minimum\":3, \"options\": [1,2,3,4,5]}";
            const string expression = "let $min = minimum in options[? @ > $min]";
            const string expected = "[4,5]";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithMapFunction()
        {
            const string input = "[{\"foo\":1}, {\"foo\":null}, {\"foo\":2}]";
            const string expression = "map(&foo, @)";
            const string expected = "[1,null,2]";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithMinByFunction()
        {
            const string input = "[{\"foo\":2}, {\"foo\":1}, {\"foo\":3}]";
            const string expression = "min_by(@, &foo)";
            const string expected = "{\"foo\":1}";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithMaxByFunction()
        {
            const string input = "[{\"foo\":2}, {\"foo\":1}, {\"foo\":3}]";
            const string expression = "max_by(@, &foo)";
            const string expected = "{\"foo\":3}";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithGroupByFunction()
        {
            const string input = "[" +
                                    "{\"k\": \"a\", \"v\": 30}," +
                                    "{\"k\": \"b\", \"v\": 20}," +
                                    "{\"k\": \"a\", \"v\": 10}" +
                                 "]";
            const string expression = "group_by(@, &k)";
            const string expected = "{" +
                                        "\"a\":[" +
                                            "{\"k\":\"a\",\"v\":30}," +
                                            "{\"k\":\"a\",\"v\":10}" +
                                        "]," +
                                        "\"b\":[" +
                                            "{\"k\":\"b\",\"v\":20}" +
                                        "]" +
                                    "}";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
        
        [Fact]
        public async Task AsyncWithSortByFunction()
        {
            const string input = "[" +
                                     "{\"k\": \"a\", \"v\": 30}," +
                                     "{\"k\": \"b\", \"v\": 20}," +
                                     "{\"k\": \"b\", \"v\": 10}" +
                                 "]";
            const string expression = "sort_by(@, &v)";
            const string expected = "[" +
                                        "{\"k\":\"b\",\"v\":10}," +
                                        "{\"k\":\"b\",\"v\":20}," +
                                        "{\"k\":\"a\",\"v\":30}" +
                                    "]";

            var syncResult = jmesPath.Transform(input, expression);
            var asyncResult = await jmesPath.TransformAsync(input, expression);

            Assert.Equal(expected, syncResult);
            Assert.Equal(syncResult, asyncResult);
        }
    }
}
