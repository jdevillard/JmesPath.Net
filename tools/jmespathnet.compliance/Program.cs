using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jmespath.net.compliance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cmdLine = CommandLine.Parse(args);

            var folder = cmdLine.TestSuitesFolder;
            var files = Directory.EnumerateFiles(folder, "*.json");

            var compliance = new Compliance();

            foreach (var path in files)
            {
                var name = Path.GetFileName(path);
                ConsoleEx.WriteLine(ConsoleColor.Cyan, $"Executing compliance test {name}...");

                var content = File.ReadAllText(path);
                compliance.RunTestSuite(content);
            }

            var total = compliance.TestResults.Count;
            var succeeded = compliance.TestResults.Count(r => r.Success);
            var failed = total - succeeded;

            var success = (double)succeeded/total;

            ConsoleEx.WriteLine(ConsoleColor.White, $"Compliance summary:");
            ConsoleEx.WriteLine(ConsoleColor.White, $"Success rate: {success:P}, {succeeded}/{total} succeeded, {failed}/{total} failed.");
        }
    }
}
