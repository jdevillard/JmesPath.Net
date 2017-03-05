using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using DevLab.JmesPath;

namespace jmespath.net.compliance
{
    public sealed class Compliance
    {
        public Compliance()
        {
            TestResults = new List<ComplianceResult>();
        }

        public List<ComplianceResult> TestResults { get; private set; }

        public void RunTestSuite(string input)
        {
            var json = JToken.Parse(input);
            var testsuites = json as JArray;
            if (testsuites == null)
                return;

            foreach (var testsuite in testsuites)
            {
                var document = testsuite["given"];

                var cases = testsuite["cases"];
                var testcases = cases as JArray;
                if (testcases == null)
                    return;

                foreach (var testcase in testcases)
                {
                    // ignoring benchmark tests

                    if (testcase["bench"] != null)
                        continue;

                    var expression = (testcase["expression"] as JValue)?.Value as String;
                    if (expression == null)
                        continue;

                    var comment = (testcase["comment"] as JValue)?.Value as String ?? "";
                    var error = (testcase["error"] as JValue)?.Value as String ?? null;

                    var expected = testcase["result"];

                    var result = RunTestCase(document, expression, expected, error);
                    TestResults.Add(result);
                }
            }
        }

        private static ComplianceResult RunTestCase(JToken document, string expression, JToken expected, string error)
        {
            ConsoleEx.Out.Write(ConsoleColor.Gray, $"{expression}...");

            var result = EvaluateExpression(document, expression);

            if (result.Success)
                result.Success = CompareJson(expected, result.Result);

            var succeeded = result.Success;
            var color = succeeded ? ConsoleColor.Green : ConsoleColor.Yellow;
            var message = succeeded ? "Succeeded" : "Failed";

            if (succeeded && error != null)
            {
                color = ConsoleColor.Yellow;
                message = $"Expected error: {error}, but completed without errors";
            }

            if (!succeeded && result.Error != null)
            {
                color = ConsoleColor.Yellow;
                message = $"Failed with unexpected error: {result.Error}.";
            }

            ConsoleEx.Out.Write(color, message);
            ConsoleEx.Out.WriteLine(ConsoleColor.Gray, ".");

            return result;
        }

        private static ComplianceResult EvaluateExpression(JToken document, string expression)
        {
            try
            {
                var parser = new JmesPath();
                var result = parser.Transform(document, expression);

                return new ComplianceResult
                {
                    Success = true,
                    Result = result,
                };
            }
            catch (Exception exception)
            {
                return new ComplianceResult
                {
                    Success = false,
                    Error = exception.Message,
                };
            }
        }

        private static bool CompareJson(JToken expected, JToken actual)
        {
            return JToken.DeepEquals(expected, actual);
        }
    }
}
