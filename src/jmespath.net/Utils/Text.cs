using System;
using System.Globalization;

namespace DevLab.JmesPath.Utils
{
    /// <summary>
    /// The <see cref="Text" /> class represents a sequence of Unicode codepoints.
    /// If differs from the .NET <see cref="String" /> class in that is correctly
    /// handles codepoints from supplementary planes, including surrogate pairs.
    /// </summary>
    internal class Text
    {
        private readonly string text_;
        private readonly StringInfo info_;

        /// <summary>
        /// Initialize a new instance of the <see cref="Text" /> class.
        /// </summary>
        /// <param name="text"></param>
        public Text(string text)
        {
            text_ = text;
            info_ = new StringInfo(text_);
        }

        /// <summary>
        /// The number of Unicode codepoints.
        /// </summary>
        public int Length
            => info_.LengthInTextElements;

        public static implicit operator String(Text text)
            => text.ToString();

        public static explicit operator Text(string text)
            => new Text(text);

        public override string ToString()
            => text_;
    }
}