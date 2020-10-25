namespace jmespath.net.tests.Parser
{
    using FactAttribute = Xunit.FactAttribute;

    public class BlockTest: ParserTestBase
    {
        [Fact]
        public void ParseBlock()
        {
            Assert("{% let var1 = name %} foo[?name == 'a']", "{\"foo\": [{\"name\": \"a\"}, {\"name\": \"b\"}]}", "[{\"name\":\"a\"}]");
        }

        [Fact]
        public void ParseVarOnly()
        {
            Assert("{% let var1 = name %}", "{\"foo\": [{\"name\": \"a\"}, {\"name\": \"b\"}]}", "{\"foo\":[{\"name\":\"a\"},{\"name\":\"b\"}]}");
        }

        [Fact]
        public void ParseSimpleVar()
        {
            Assert("{% let var1 = name %} variable('var1')", "{\"name\": \"a\"}", "\"a\"");
        }

        [Fact]
        public void ParseObjectVar()
        {
            Assert("{% let var1 = foo %} variable('var1')", "{\"foo\": [{\"name\": \"a\"}, {\"name\": \"b\"}]}", "[{\"name\":\"a\"},{\"name\":\"b\"}]");
        }

        [Fact]
        public void ParseObjectVarWithIdentifier()
        {
            Assert("{% let var1 = foo %} variable('var1')[0]", "{\"foo\": [{\"name\": \"a\"}, {\"name\": \"b\"}]}", "{\"name\":\"a\"}");
        }

        [Fact]
        /*
         * This test show the syntaxt to access a "Parent Property" inside a Projection using a variable
         * The input JSON is the following
         {
            "id": "123",
            "foo": [
                {
                    "name": "a"
                },
                {
                    "name": "b"
                }
            ]
        }
         * The expected JSON is the following
        [
            {
                "parentId": "123",
                "name": "a"
            },
            {
                "parentId": "123",
                "name": "b"
            }
        ] 
         * 
         */
        public void BlockOuterScope()
        {
            Assert("{% let var1 = id %} foo[].{parentId:variable('var1'), name:name} ", "{\"id\":\"123\",\"foo\": [{\"name\": \"a\"}, {\"name\": \"b\"}]}", "[{\"parentId\":\"123\",\"name\":\"a\"},{\"parentId\":\"123\",\"name\":\"b\"}]");
        }
    }
}