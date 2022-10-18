using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DevLab.JmesPath.Utils
{
    /// <summary>
    /// The <see cref="Text" /> class represents a sequence of Unicode codepoints.
    /// If differs from the .NET <see cref="String" /> class in that is correctly
    /// handles codepoints from supplementary planes, including surrogate pairs.
    /// </summary>
    internal sealed partial class Text : IEnumerable<string>, IEquatable<Text>, IComparable<Text>
    {
        private readonly string text_;
        private readonly StringInfo info_;

        private static readonly IComparer<Text> defaultComparer_
            = new TextComparer();

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
        /// Initialize a new instance of the <see cref="Text" /> class.
        /// </summary>
        /// <param name="codePoints"></param>
        public Text(params int[] codePoints)
        {
            var sb = new StringBuilder();
            foreach (var codePoint in codePoints)
                sb.Append(Char.ConvertFromUtf32(codePoint));

            text_ = sb.ToString();
            info_ = new StringInfo(text_);
        }

        /// <summary>
        /// Returns an <see cref="IComparer{Text}" /> implementation
        /// that compares Text using the numerical value of its codepoints.
        /// </summary>
        public static IComparer<Text> CodePointComparer
            => defaultComparer_;

        /// <summary>
        /// The number of Unicode codepoints.
        /// </summary>
        public int Length
            => info_.LengthInTextElements;

        /// <summary>
        /// Returns a enumerator over the sequence of Unicode codepoints.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<int> GetCodePointsEnumerator()
            => new CodePointEnumerator(this);

        /// <summary>
        /// The sequence of Unicode codepoints.
        /// </summary>
        public IEnumerable<int> CodePoints
            => new CodePointEnumerator(this).AsEnumerable();

        public static implicit operator String(Text text)
            => text.ToString();

        public static explicit operator Text(string text)
            => new Text(text);

        public IEnumerator<string> GetEnumerator()
            => new TextEnumerator(text_);

        /// <summary>
        /// Returns true if the two strings are equal
        /// i.e. if the two sequences of Unicode codepoints
        /// are identical.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Text other)
            => CompareTo(other) == 0;

        /// <summary>
        /// Compares the two sequences of Unicode codepoints.
        /// A string will sort based on the numerical value
        /// of its first differring codepoint.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int CompareTo(Text other)
        {
            var length = Math.Min(this.Length, other.Length);
            var codePoints = this.CodePoints.ToArray();
            var otherCodePoints = other.CodePoints.ToArray();

            for (var index = 0; index < length; index++)
            {
                if (codePoints[index] < otherCodePoints[index])
                    return -1;
                else if (codePoints[index] > otherCodePoints[index])
                    return 1;
            }

            if (codePoints.Length < otherCodePoints.Length)
                return -1;
            else if (codePoints.Length > otherCodePoints.Length)
                return 1;

            return 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public override string ToString()
            => text_;
    }
}