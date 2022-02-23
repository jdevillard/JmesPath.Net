using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace jmespath.net.compliance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cmdLine = CommandLine.Parse(args);

            var json = cmdLine.OutputFormat == OutputFormats.Json;
            var text = cmdLine.OutputFormat == OutputFormats.Text;

            var folder = cmdLine.TestSuitesFolder;

            var files = Enumerable.Empty<string>();
            if (Directory.Exists(folder))
                files = Directory.EnumerateFiles(folder, "*.json");
            else if (File.Exists(folder))
                files = new[] { folder };


            var compliance = new Compliance();
            var functions = new Compliance();

            foreach (var path in files)
            {
                var name = Path.GetFileName(path);
                if (text)
                {
                    ConsoleEx.WriteLine(ConsoleColor.Cyan, $"Executing compliance test {name}...");
                }

                var content = File.ReadAllText(path, new UTF8Encoding(false));
                if (name == "functions.json")
                {
                    functions.RunTestSuite(content, logTextOut: text);
                }
                else
                {
                    compliance.RunTestSuite(content, logTextOut: text);
                }
            }

            var total = compliance.TestResults.Concat(functions.TestResults).Count();
            var succeeded = compliance.TestResults.Concat(functions.TestResults).Count(r => r.Success);
            var failed = total - succeeded;

            var success = (double)succeeded / total;

            if (text)
            {
                ConsoleEx.WriteLine(ConsoleColor.White, $"Compliance summary:");
                ConsoleEx.WriteLine(ConsoleColor.White, $"Success rate: {success:P}, {succeeded}/{total} succeeded, {failed}/{total} failed.");
            }

            var totalFunctions = functions.TestResults.Count;
            var succeededFunctions = functions.TestResults.Count(r => r.Success);
            var failedFunctions = totalFunctions - succeededFunctions;

            var successFunctions = (double)succeededFunctions / totalFunctions;

            if (json)
            {
                var serializer = new JsonSerializerSettings
                {
                    Formatting = Formatting.None,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                var report = new ComplianceReport
                {
                    Compliance = 100.0 * success,
                    FunctionSets = new []
                    {
                        new FunctionSet {
                            Version = "(default)",
                            Compliance = 100.0 * successFunctions,
                        },
                    }
                };
                var output = JsonConvert.SerializeObject(report, serializer);
                Console.WriteLine(output);
            }

            var exitCode = succeeded == total ? 0 : 1;
            Environment.Exit(exitCode);
        }
    }
}
