namespace DevLab.JmesPath
{
    using System;
    using System.IO;
    using System.Text;

    public static class Parser
    {
        public static void Parse(Stream stream, Encoding encoding, IJmesPathGenerator generator)
        {
            var scanner = new JmesPathScanner(stream, encoding.CodePage.ToString());
            scanner.InitializeLookaheadQueue();

            var analyzer = new JmesPathParser(scanner, generator);
            if (!analyzer.Parse())
            {
                System.Diagnostics.Debug.Assert(false);
                throw new Exception("Error: syntax.");
            }
        }
    }
}
