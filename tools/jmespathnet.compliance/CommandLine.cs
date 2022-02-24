using System;
using System.Collections.Generic;
using NDesk.Options;

namespace jmespath.net.compliance
{
    public enum OutputFormats
    {
        Text,
        Json,
    }

    public class CommandLine
    {
        public string TestSuitesFolder { get; set; }
        public OutputFormats OutputFormat { get; set; }

        private CommandLine()
        {
            OutputFormat = OutputFormats.Text;
        }

        public static CommandLine Parse(string[] args)
        {
            var commandLine = new CommandLine();

            var options = new OptionSet
            {
                { "t|tests|test-suites=", v => commandLine.TestSuitesFolder = v },
                { "o|output=", v => commandLine.OutputFormat = ParseOutputFormat(v) },
            };

            try
            {
                var remaining = options.Parse(args);
                ParseRemainingArguments(remaining);

            }
            catch (OptionException e)
            {
                Console.Error.WriteLine(e.Message);
            }

            return commandLine;
        }

        private static OutputFormats ParseOutputFormat(string v)
        {
            var outputFormat = OutputFormats.Text;
            if (Enum.TryParse<OutputFormats>(v, true, out var _outputFormat))
                outputFormat = _outputFormat;

            return outputFormat;
        }

        private static void ParseRemainingArguments(List<string> remaining)
        {
        }
    }
}