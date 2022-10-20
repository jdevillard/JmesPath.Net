using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace DevLab.JmesPath.Utils
{
    /// <summary>
    /// An <see cref="IEnumerator{string}" /> implementation
    /// that enumerates Unicode characters in a string while
    /// correctly handling surrogate pairs.
    /// </summary>
    internal sealed class TextEnumerator : IEnumerator<string>
    {
        private readonly TextElementEnumerator enum_;

        /// <summary>
        /// Initialize a new instance of the <see cref="TextEnumerator" /> class.
        /// </summary>
        /// <param name="text"></param>
        public TextEnumerator(String text)
        {
            enum_ = StringInfo.GetTextElementEnumerator(text);
        }

        public string Current
            => enum_.GetTextElement();

        object IEnumerator.Current
            => Current;

        public bool MoveNext()
            => enum_.MoveNext();

        public void Reset()
            => enum_.Reset();

        public void Dispose() { }
    }
}