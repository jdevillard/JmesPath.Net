using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DevLab.JmesPath;

namespace DevLab.JmesPath
{
    internal partial class JmesPathScanner
    {
        public JmesPathScanner(Stream file, int codepage)
        {
            SetSource(file, codepage);
        }
    }
}
