using Newtonsoft.Json.Linq;
using Xunit;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;

namespace jmespath.net.tests.Expressions
{
    public class JmesPathProjectionTest
    {
        /*
         * http://jmespath.org/specification.html#flatten-operator
         * 
         * search([], [[0, 1], 2, [3], 4, [5, [6, 7]]]) -> [0, 1, 2, 3, 4, 5, [6, 7]]
         * search([], [0, 1, 2, 3, 4, 5, [6, 7]]) -> [0, 1, 2, 3, 4, 5, 6, 7]
         * search([][], [[0, 1], 2, [3], 4, [5, [6, 7]]]) -> [0, 1, 2, 3, 4, 5, 6, 7]
         * 
         */

        [Fact]
        public void JmesPathProjection_Flatten()
        {
            JmesPathProjection_Transform(new JmesPathFlattenProjection(), "[[0, 1], 2, [3], 4, [5, [6, 7]]]", "[0,1,2,3,4,5,[6,7]]");
            JmesPathProjection_Transform(new JmesPathFlattenProjection(), "[0, 1, 2, 3, 4, 5, [6, 7]]", "[0,1,2,3,4,5,6,7]");
            JmesPathProjection_Transform(new JmesPathIndexExpression(new JmesPathFlattenProjection(), new JmesPathFlattenProjection()), "[[0, 1], 2, [3], 4, [5, [6, 7]]]", "[0,1,2,3,4,5,6,7]");
        }

        /*
         * http://jmespath.org/specification.html#wildcard-expressions
         * 
         * search([*].foo, [{"foo": 1}, {"foo": 2}, {"foo": 3}]) -> [1, 2, 3]
         * search([*].foo, [{"foo": 1}, {"foo": 2}, {"bar": 3}]) -> [1, 2]
         * 
         * search([*][0], ["foo", "bar"]) -> []
         * 
         */

        [Fact]
        public void JmesPathProjection_Wildcard_List()
        {
            JmesPathExpression identifier = new JmesPathIdentifier("foo");
            JmesPathProjection wildcard = new JmesPathListWildcardProjection();
            JmesPathExpression expression = new JmesPathSubExpression(wildcard, identifier);

            JmesPathProjection_Transform(expression, "[{\"foo\": 1}, {\"foo\": 2}, {\"foo\": 3}]", "[1,2,3]");
            JmesPathProjection_Transform(expression, "[{\"foo\": 1}, {\"foo\": 2}, {\"bar\": 3}]", "[1,2]");

            identifier = new JmesPathIndex(new JmesPathNumber(0));
            wildcard = new JmesPathListWildcardProjection();
            expression = new JmesPathIndexExpression(wildcard, identifier);

            JmesPathProjection_Transform(expression, "[\"foo\", \"bar\"]", "[]");
        }

        /*
         * http://jmespath.org/specification.html#wildcard-expressions
         * 
         * search(*.foo, {"a": {"foo": 1}, "b": {"foo": 2}, "c": {"bar": 1}}) -> [1, 2]
         * 
         */

        [Fact]
        public void JmesPathProjection_Wildcard_Hash()
        {
            var identifier = new JmesPathIdentifier("foo");
            var wildcard = new JmesPathHashWildcardProjection();
            var expression = new JmesPathSubExpression(wildcard, identifier);

            JmesPathProjection_Transform(expression, "{\"a\": {\"foo\": 1}, \"b\": {\"foo\": 2}, \"c\": {\"bar\": 1}}", "[1,2]");
        }

        /*
         * Other complex cases
         * 
         * search(toto[][].first[][], { "toto": [{"first": 0}, {"first": 1}, {"first": 2}, {"first": 3}] }) -> [0, 1, 2, 3]
         * search(toto[][].first[][], { "toto": [[{"first": [[0, 1], 2, [3, 4]]}, {"first": 5}], [{"first": 6}], [[{"first": 7}]]] }) -> [0, 1, 2, 3, 4, 5, 6, 7]
         * 
         */

        [Fact]
        public void JmesPathProjection_Others()
        {
            var expression = new JmesPathIndexExpression(
                new JmesPathIndexExpression(
                    new JmesPathSubExpression(new JmesPathIndexExpression(
                        new JmesPathIndexExpression(
                            new JmesPathIdentifier("toto"),
                            new JmesPathFlattenProjection()),
                        new JmesPathFlattenProjection()),
                        new JmesPathIdentifier("first")),
                    new JmesPathFlattenProjection()),
                new JmesPathFlattenProjection()
                );

            JmesPathProjection_Transform(expression, "{ \"toto\": [{\"first\": 0}, {\"first\": 1}, {\"first\": 2}, {\"first\": 3}] }", "[0,1,2,3]");
            JmesPathProjection_Transform(expression, "{ \"toto\": [[{\"first\": [[0, 1], 2, [3, 4]]}, {\"first\": 5}], [{\"first\": 6}], [[{\"first\": 7}]]] }", "[0,1,2,3,4,5,6,7]");
        }

        public void JmesPathProjection_Transform(JmesPathExpression expression, string input, string expected)
        {
            var token = JToken.Parse(input);
            var result = expression.Transform(token);
            var actual = result.Token.AsString();

            Assert.Equal(expected, actual);
        }
    }
}
