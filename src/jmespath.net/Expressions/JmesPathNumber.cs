using System;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    /// <summary>
    /// Represents a JmesPath number.
    /// </summary>
    public class JmesPathNumber : JmesPathExpression
    {
        public JmesPathNumber(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public override JToken Transform(JToken json)
        {
            System.Diagnostics.Debug.Assert(false);
            throw new NotImplementedException();
        }
    }
}