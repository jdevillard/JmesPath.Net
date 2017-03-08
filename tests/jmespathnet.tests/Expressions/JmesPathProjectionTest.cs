using DevLab.JmesPath.Expressions;

namespace jmespath.net.tests.Expressions
{
    using FactAttribute = Xunit.FactAttribute;

    public class JmesPathProjectionTest : JmesPathExpressionsTestBase
    {
        [Fact]
        public void JmesPathProjection_Slice()
        {
            // expression foo[0:2].a

            JmesPathExpression expression = new JmesPathSubExpression(
                new JmesPathIndexExpression(
                    new JmesPathIdentifier("foo"),
                    new JmesPathSliceProjection(0, 2, null)
                    ),
                new JmesPathIdentifier("a")
                );

            Assert(expression, "{\"foo\": [{\"a\": 1}, {\"a\": 2}, {\"a\": 3}, {\"a\": 4}]}", "[1,2]");

            // expression foo[0:2][0:1]

            expression = new JmesPathIndexExpression(
                new JmesPathIndexExpression(
                    new JmesPathIdentifier("foo"),
                    new JmesPathSliceProjection(0, 2, null)
                    ),
                new JmesPathSliceProjection(0, 1, null)
                );

            Assert(expression, "{\"foo\": [[1, 2, 3], [4, 5, 6]]}", "[[1],[4]]");
        }

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
            Assert(new JmesPathFlattenProjection(), "[[0, 1], 2, [3], 4, [5, [6, 7]]]", "[0,1,2,3,4,5,[6,7]]");
            Assert(new JmesPathFlattenProjection(), "[0, 1, 2, 3, 4, 5, [6, 7]]", "[0,1,2,3,4,5,6,7]");
            Assert(new JmesPathIndexExpression(new JmesPathFlattenProjection(), new JmesPathFlattenProjection()), "[[0, 1], 2, [3], 4, [5, [6, 7]]]", "[0,1,2,3,4,5,6,7]");
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

            Assert(expression, "[{\"foo\": 1}, {\"foo\": 2}, {\"foo\": 3}]", "[1,2,3]");
            Assert(expression, "[{\"foo\": 1}, {\"foo\": 2}, {\"bar\": 3}]", "[1,2]");

            identifier = new JmesPathIndex(0);
            wildcard = new JmesPathListWildcardProjection();
            expression = new JmesPathIndexExpression(wildcard, identifier);

            Assert(expression, "[\"foo\", \"bar\"]", "[]");
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
            var expression = new JmesPathSubExpression(
                new JmesPathHashWildcardProjection(),
                new JmesPathIdentifier("foo")
                );

            Assert(expression, "{\"a\": {\"foo\": 1}, \"b\": {\"foo\": 2}, \"c\": {\"bar\": 1}}", "[1,2]");
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

            Assert(expression, "{ \"toto\": [{\"first\": 0}, {\"first\": 1}, {\"first\": 2}, {\"first\": 3}] }", "[0,1,2,3]");
            Assert(expression, "{ \"toto\": [[{\"first\": [[0, 1], 2, [3, 4]]}, {\"first\": 5}], [{\"first\": 6}], [[{\"first\": 7}]]] }", "[0,1,2,3,4,5,6,7]");
        }

        /*
         * Compliance
         * 
         * search(foo.*.bar[0], { "foo": {\"a\": {\"bar\": [0, 2]}, \"b\": {\"bar\": [1, 3]}}}) -> [0, 1]
         * 
         */

        [Fact]
        public void JmesPathProjection_Compliance()
        {
            var expression = new JmesPathIndexExpression(
                new JmesPathSubExpression(
                    new JmesPathSubExpression(
                        new JmesPathIdentifier("foo"),
                        new JmesPathHashWildcardProjection()
                        ),
                    new JmesPathIdentifier("bar")),
                new JmesPathIndex(0)
                );

            Assert(expression, "{ \"foo\": {\"a\": {\"bar\": [0, 2]}, \"b\": {\"bar\": [1, 3]}}}", "[0,1]");
        }
    }
}
