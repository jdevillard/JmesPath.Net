using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DevLab.JmesPath.Interop;

namespace jmespath.net.compliance
{
    using Registrations = Func<IRegisterFunctions, IRegisterFunctions>;

    public class Program
    {
        public static void Main(string[] args)
        {
            var cmdLine = CommandLine.Parse(args);

            var json = cmdLine.OutputFormat == OutputFormats.Json;
            var text = cmdLine.OutputFormat == OutputFormats.Text;

            var registrations = cmdLine.Registrations;
            var folder = cmdLine.TestSuitesFolder;

            var functions = GetThirdPartyRegistrations(registrations);

            // run test suite

            var files = EnumerateComplianceTests(folder);
            var (compliance, complianceFuncs) = CheckCompliance(files, text, functions);

            ReportComplianceResults(
                  compliance
                , complianceFuncs
                , text
                , json);
        }

        private static Registrations[] GetThirdPartyRegistrations(IList<string> registrations)
        {
            // a registration is a type name for a class implementing the following method:
            // public static IRegisterFunctions Register(IRegisterFunctions repository);
            //

            Registrations[] factories =
                    registrations
                        .Select(LoadRegistrationFactory)
                        .ToArray()
                ;

            return factories;
        }

        private static Registrations LoadRegistrationFactory(string assemblyQualifiedName)
        {
            if (!AssemblyQualifiedNameParser.TryParse(assemblyQualifiedName, out var assemblyName, out var typeName))
                throw new Exception($"Unable to load third-party functions {assemblyQualifiedName}");

            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fileName = $"{assemblyName}.dll";
            var path = Path.Combine(directory, fileName);

            var type = Assembly
                    .LoadFile(path)
                    .GetType(typeName)
                ;

            var bindingFlags = BindingFlags.Public | BindingFlags.Static;
            var method = type.GetMethod("Register", bindingFlags);
            if (method == null)
                throw new Exception($"Unable to load third-party functions {assemblyQualifiedName}");

            Registrations func =
                reg => method.Invoke(null, new object[] { reg, }) as IRegisterFunctions;

            return func;
        }

        private static IEnumerable<string> EnumerateComplianceTests(string folder)
        {
            var files = Enumerable.Empty<string>();
            if (Directory.Exists(folder))
                files = Directory.EnumerateFiles(folder, "*.json");
            else if (File.Exists(folder))
                files = new[] { folder };
            return files;
        }

        private static (ComplianceSummary compliance, ComplianceSummary complianceFunctions) CheckCompliance(IEnumerable<string> files, bool logTextOut, Registrations[] functions)
        {
            var compliance = new Compliance();
            var complianceFuncs = new Compliance();

            foreach (var path in files)
            {
                var name = Path.GetFileName(path);
                if (logTextOut)
                {
                    ConsoleEx.WriteLine(ConsoleColor.Cyan, $"Executing compliance test {name}...");
                }

                var content = File.ReadAllText(path, new UTF8Encoding(false));
                if (name == "functions.json")
                {
                    complianceFuncs.RunTestSuite(content, logTextOut);
                }
                else
                {
                    compliance.RunTestSuite(content, logTextOut, functions);
                }
            }

            var total = compliance.TestResults.Concat(complianceFuncs.TestResults).Count();
            var succeeded = compliance.TestResults.Concat(complianceFuncs.TestResults).Count(r => r.Success);
            var failed = total - succeeded;

            var percent = (double)succeeded / total;

            var totalFunctions = complianceFuncs.TestResults.Count;
            var succeededFunctions = complianceFuncs.TestResults.Count(r => r.Success);
            var failedFunctions = totalFunctions - succeededFunctions;

            var percentFunctions = (double)succeededFunctions / totalFunctions;

            return (
                new ComplianceSummary(percent, succeeded, total, failed),
                new ComplianceSummary(percentFunctions, succeededFunctions, totalFunctions, failedFunctions)
                );
        }

        private static void ReportComplianceResults(
            ComplianceSummary complianceSummary,
            ComplianceSummary complianceSummaryFuncs,
            bool logTextOut = true,
            bool logJsonOut = false
        )
        {
            var s = complianceSummary;
            var f = complianceSummaryFuncs;

            if (logTextOut)
            {
                ConsoleEx.WriteLine(ConsoleColor.White, $"Compliance summary:");
                ConsoleEx.WriteLine(ConsoleColor.White, $"Success rate: {s.Percent:P}, {s.Succeeded}/{s.Total} succeeded, {s.Failed}/{s.Total} failed.");
            }

            if (logJsonOut)
            {
                var serializer = new JsonSerializerSettings
                {
                    Formatting = Formatting.None,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                var report = new ComplianceReport
                {
                    Compliance = 100.0 * s.Percent,
                    FunctionSets = new[]
                    {
                        new FunctionSet {
                            Version = "(default)",
                            Compliance = 100.0 * f.Percent,
                        },
                    }
                };
                var output = JsonConvert.SerializeObject(report, serializer);
                Console.WriteLine(output);
            }
        }
    }
}
