using System;
using System.Collections.Generic;
using NDesk.Options;

namespace jmespath.net.compliance
{
    public class CommandLine
    {
        public string TestSuitesFolder { get; set; }

        private CommandLine()
        {
        }

        public static CommandLine Parse(string[] args)
        {
            var commandLine = new CommandLine();

            var options = new OptionSet
            {
                { "t|tests|test-suites=", v => commandLine.TestSuitesFolder = v },
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

        private static void ParseRemainingArguments(List<string> remaining)
        {
        }
    }
}