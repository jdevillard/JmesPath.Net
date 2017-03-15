using System;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public abstract class MathArrayFunction : JmesPathFunction
    {
        protected MathArrayFunction(string name, int count) 
            : base(name, count)
        {
        }

        protected MathArrayFunction(string name, int minCount, bool variadic) 
            : base(name, minCount, variadic)
        {
        }

        public override bool Validate(params JToken[] args)
        {
            if (args[0].Type != JTokenType.Array)
                throw new Exception("invalid-type");

            foreach (var item in (JArray)args[0])
                if (item.Type != JTokenType.Integer && item.Type != JTokenType.Float)
                    throw new Exception("invalid-type");

            return true;
        }
    }
}