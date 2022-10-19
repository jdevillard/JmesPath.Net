using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace DevLab.JmesPath.Utils
{
    /// <summary>
    /// An <see cref="IEnumerator{string}" /> implementation
    /// that enumerates Unicode codepoints in a string.
    /// </summary>
    internal sealed class CodePointEnumerator : IEnumerator<int>
    {
        private readonly string text_;

        private int[] codePoints_;
        private int index_ = -1;

        /// <summary>
        /// Initialize a new instance of the <see cref="CodePointEnumerator" /> class.
        /// </summary>
        /// <param name="text"></param>
        public CodePointEnumerator(Text text)
        {
            text_ = text;
            codePoints_ = GetCodePoints(text);
        }

        public IEnumerable<int> AsEnumerable()
        {
            while (MoveNext())
                yield return Current;
        }

        public int Current
            => codePoints_[index_];

        object IEnumerator.Current
            => Current;

        public bool MoveNext()
            => ++index_ < codePoints_.Length;

        public void Reset()
        {
            index_ = -1;
        }

        public void Dispose() { }

        internal static int[] GetCodePoints(Text text)
        {
            var codePoints = new List<int>();

            var enumerator = StringInfo.GetTextElementEnumerator(text);
            while (enumerator.MoveNext())
            {
                var element = enumerator.GetTextElement();

                // element represents either a codepoint from the basic multilingual plane
                // or a supplementary plane encoded as a pair of surrogate UTF-16 code units.

                if (element.Length > 1 && (Char.IsSurrogatePair(element[0], element[1])))
                {
                    System.Diagnostics.Debug.Assert(element.Length == 2);
                    codePoints.Add(Char.ConvertToUtf32(element[0], element[1]));
                }

                else
                {
                    foreach (var codePoint in element)
                        codePoints.Add(codePoint);
                }
            }

            return codePoints.ToArray();
        }
    }
}