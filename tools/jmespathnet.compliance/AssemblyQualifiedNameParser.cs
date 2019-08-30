using System.Reflection;
using System.Text.RegularExpressions;

namespace jmespath.net.compliance
{
    public sealed class AssemblyQualifiedNameParser
    {
        // no sane way to parse an assembly qualified name
        // https://stackoverflow.com/questions/1410312/parsing-assembly-qualified-name

        private const string Pattern = @"^(?<type>.*?),\ ?(?<assembly>[^,]+(?:,\ ?Version=[^,]+)?(?:,\ ?Culture=[^,]+)?(?:,\ ?PublicKeyToken=.*)?)$";
        private static Regex AssemblyQualifiedNameRegex = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.Singleline);

        public static bool TryParse(string assemblyQualifiedName, out AssemblyName assemblyName, out string typeName)
        {
            assemblyName = new AssemblyName();
            typeName = null;

            var match = AssemblyQualifiedNameRegex.Match(assemblyQualifiedName);
            if (match.Success)
            {
                assemblyName = new AssemblyName(match.Groups["assembly"].Value);
                typeName = match.Groups["type"].Value;
            }

            return match.Success;
        }
    }
}