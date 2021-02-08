using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using DevLab.JmesPath;
using DevLab.JmesPath.Interop;

namespace jmespath.net.compliance
{
    using Registrations = Func<IRegisterFunctions, IRegisterFunctions>;

    public class Program
    {
        public static void Main(string[] args)
        {
            var cmdLine = CommandLine.Parse(args);
            var registrations = cmdLine.Registrations;
            var folder = cmdLine.TestSuitesFolder;

            var functions = GetThirdPartyRegistrations(registrations);

            // run test suite

            var files = EnumerateComplianceTests(folder);
            var (success, succeeded, total, failed) = CheckCompliance(files, functions);

            ReportComplianceResults(
                  success
                , succeeded
                , total
                , failed
                );
        }

        private static Registrations[] GetThirdPartyRegistrations(IList<string> registrations)
        {
            // a registration is a type name for a class implementing the following method:
            // public static IRegisterFunctions Register(IRegisterFunctions repository);
            //

            Func<IRegisterFunctions, IRegisterFunctions>[] factories =
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

            Func<IRegisterFunctions, IRegisterFunctions> func =
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

        private static (double success, int succeeded, int total, int failed) CheckCompliance(IEnumerable<string> files, Registrations[] functions)
        {
            var compliance = new Compliance();

            foreach (var path in files)
            {
                var name = Path.GetFileName(path);
                ConsoleEx.WriteLine(ConsoleColor.Cyan, $"Executing compliance test {name}...");

                var content = File.ReadAllText(path, new UTF8Encoding(false));
                compliance.RunTestSuite(content, functions);
            }

            var total = compliance.TestResults.Count;
            var succeeded = compliance.TestResults.Count(r => r.Success);
            var failed = total - succeeded;

            var success = (double)succeeded / total;

            var result = (success, succeeded, total, failed);
            return result;
        }

        private static void ReportComplianceResults(double success, int succeeded, int total, int failed)
        {
            ConsoleEx.WriteLine(ConsoleColor.White, $"Compliance summary:");
            ConsoleEx.WriteLine(ConsoleColor.White,
                $"Success rate: {success:P}, {succeeded}/{total} succeeded, {failed}/{total} failed.");
        }
    }
}
