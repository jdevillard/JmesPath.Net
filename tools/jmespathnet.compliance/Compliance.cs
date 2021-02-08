using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using DevLab.JmesPath;
using DevLab.JmesPath.Functions;
using DevLab.JmesPath.Interop;

namespace jmespath.net.compliance
{
    using Registrations = Func<IRegisterFunctions, IRegisterFunctions>;

    public sealed class Compliance
    {
        public Compliance()
        {
            TestResults = new List<ComplianceResult>();
        }

        public List<ComplianceResult> TestResults { get; private set; }

        public void RunTestSuite(string input, Func<IRegisterFunctions, IRegisterFunctions>[] functions)
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

                    var result = RunTestCase(functions, document, expression, expected, error);
                    TestResults.Add(result);
                }
            }
        }

        private static ComplianceResult RunTestCase(Registrations[] registrationFactories, JToken document, string expression, JToken expected, string error)
        {
            ConsoleEx.Out.Write(ConsoleColor.Gray, $"{expression}...");

            var result = EvaluateExpression(registrationFactories, document, expression);

            var message = new StringBuilder();
            message.AppendFormat("Evaluation {0} ; ", result.Success ? "succeeded" : "failed");

            if (result.Success)
            {
                result.Success = CompareJson(expected, result.Result);
                message.AppendFormat("Comparison {0} ; ", result.Success ? "succeeded" : "failed");
            }

            var succeeded = result.Success;
            var color = succeeded ? ConsoleColor.Green : ConsoleColor.Yellow;

            if (succeeded && error != null)
            {
                color = ConsoleColor.Yellow;
                message.Append( $"Expected error: {error}, but completed without errors");
            }

            if (!succeeded)
            {
                if (error == null)
                {
                    color = ConsoleColor.Yellow;
                    message.Append($"Failed with unexpected error: {result.Error}.");
                }

                else if (result.Error == null || !IsExpectedError(result.Error, error))
                {
                    color = ConsoleColor.Yellow;
                    message.Append($"Expected error: {error}, but completed without errors");
                }

                else
                {
                    result.Success = true;
                    color = ConsoleColor.Green;
                    message.Append($"Error: {error}");
                }
            }

            ConsoleEx.Out.Write(color, message.ToString());
            ConsoleEx.Out.WriteLine(ConsoleColor.Gray, ".");

            return result;
        }

        private static bool IsExpectedError(string result, string error)
        {
            var expected = error.Replace("-", " ").ToLowerInvariant();
            var actual = result.Replace("-", " ").ToLowerInvariant();

            return actual.Contains(expected);
        }

        private static ComplianceResult EvaluateExpression(Registrations[] registrationFactories, JToken document, string expression)
        {
            try
            {
                var parser = new JmesPath();

                parser.FunctionRepository
                    .Register<ItemsFunction>()
                    .Register<ToObjectFunction>()
                    .Register<ZipFunction>()
                    ;

                foreach (var registrationFactory in registrationFactories)
                    registrationFactory(parser.FunctionRepository)
                        ;

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
