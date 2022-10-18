using System.Collections.Generic;

namespace DevLab.JmesPath.Utils
{
    internal sealed partial class Text
    {
        public sealed class TextComparer : IComparer<Text>
        {
            int IComparer<Text>.Compare(Text x, Text y)
                => x.CompareTo(y);
        }
    }
}