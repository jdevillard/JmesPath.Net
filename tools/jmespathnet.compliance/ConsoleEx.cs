using System;
using System.IO;

namespace jmespath.net.compliance
{
    /// <summary>
    /// Provides color support to Console output.
    /// The methods of this class are thread-safe to 
    /// prevent color corruption in case of crash.
    /// </summary>
    public class ConsoleEx
    {
        private static readonly object sync_;
        private static readonly Writer out_;
        private static readonly Writer err_;

        static ConsoleEx()
        {
            sync_ = new Object();
            out_ = new Writer(Console.Out, sync_);
            err_ = new Writer(Console.Error, sync_);
        }

        /// <summary>
        /// Gets the Console STDOUT writer.
        /// </summary>
        public static Writer Out => out_;

        /// <summary>
        /// Gets the Console STDERR writer.
        /// </summary>
        public static Writer Error => err_;

        public sealed class Writer
        {
            private readonly TextWriter writer_;
            private readonly Object synLock_;

            /// <summary>
            /// Initialize a new instance of the <see cref="Writer"/> class.
            /// </summary>
            /// <param name="writer"></param>
            /// <param name="synLock"></param>
            public Writer(TextWriter writer, object synLock)
            {
                writer_ = writer;
                synLock_ = synLock;

                DefaultColor = ConsoleColor.Gray;
            }

            /// <summary>
            /// Gets or sets the default Console color.
            /// </summary>
            public ConsoleColor DefaultColor { get; set; }

            /// <summary>
            /// Writes a portion of text on the Console with the default color.
            /// </summary>
            /// <param name="text"></param>
            /// <param name="args"></param>
            public void Write(string text, params object[] args)
            {
                Write(DefaultColor, text, args);
            }

            /// <summary>
            /// Writes a line of text on the Console with the default color.
            /// </summary>
            /// <param name="text"></param>
            /// <param name="args"></param>
            public void WriteLine(string text, params object[] args)
            {
                WriteLine(DefaultColor, text, args);
            }

            /// <summary>
            /// Writes a colorized line of text on the Console.
            /// </summary>
            /// <param name="color"></param>
            /// <param name="text"></param>
            /// <param name="args"></param>
            public void WriteLine(ConsoleColor color, string text, params object[] args)
            {
                Write(color, text + Environment.NewLine, args);
            }

            /// <summary>
            /// Writes a gray line of text on the Console.
            /// </summary>
            /// <param name="text"></param>
            /// <param name="args"></param>
            public void WriteInfo(string text, params object[] args)
            {
                WriteLine(ConsoleColor.Gray, text, args);
            }

            /// <summary>
            /// Writes a green line of text on the Console.
            /// </summary>
            /// <param name="text"></param>
            /// <param name="args"></param>
            public void WriteSuccess(string text, params object[] args)
            {
                WriteLine(ConsoleColor.Green, text, args);
            }

            /// <summary>
            /// Writes a yellow line of text on the Console.
            /// </summary>
            /// <param name="text"></param>
            /// <param name="args"></param>
            public void WriteWarning(string text, params object[] args)
            {
                WriteLine(ConsoleColor.Yellow, text, args);
            }

            /// <summary>
            /// Writes a red line of text on the Console.
            /// </summary>
            /// <param name="text"></param>
            /// <param name="args"></param>
            public void WriteError(string text, params object[] args)
            {
                WriteLine(ConsoleColor.Red, text, args);
            }

            public void Write(ConsoleColor color, string text, params object[] args)
            {
                lock (synLock_)
                {
                    var currentColor = Console.ForegroundColor;
                    try
                    {
                        Console.ForegroundColor = color;
                        if (args?.Length > 0)
                            Console.Write(text, args);
                        else
                            Console.Write(text);
                    }
                    finally
                    {
                        Console.ForegroundColor = currentColor;
                    }
                }
            }
        }

        /// <summary>
        /// Writes a portion of text on the Console with the default color.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
        public static void Write(string text, params object[] args)
        {
            Out.Write(text, args);
        }

        /// <summary>
        /// Writes a line of text on the Console with the default color.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
        public static void WriteLine(string text, params object[] args)
        {
            Out.WriteLine(text, args);
        }

        /// <summary>
        /// Writes a colorized line of text on the Console.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="text"></param>
        /// <param name="args"></param>
        public static void WriteLine(ConsoleColor color, string text, params object[] args)
        {
            Out.WriteLine(color, text, args);
        }

        /// <summary>
        /// Writes a gray line of text on the Console.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
        public static void WriteInfo(string text, params object[] args)
        {
            Out.WriteInfo(text, args);
        }

        /// <summary>
        /// Writes a green line of text on the Console.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
        public static void WriteSuccess(string text, params object[] args)
        {
            Out.WriteSuccess(text, args);
        }

        /// <summary>
        /// Writes a yellow line of text on the Console.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
        public static void WriteWarning(string text, params object[] args)
        {
            Out.WriteWarning(text, args);
        }

        /// <summary>
        /// Writes a red line of text on the Console.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
        public static void WriteError(string text, params object[] args)
        {
            Error.WriteError(text, args);
        }

        public static void Write(ConsoleColor color, string text, params object[] args)
        {
            Out.Write(color, text, args);
        }
    }
}