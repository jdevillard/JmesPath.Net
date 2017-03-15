using System;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class TypeFunction : JmesPathFunction
    {
        public TypeFunction()
            : base("type", 1)
        {

        }
        public override bool Validate(params JToken[] args)
        {
            return true;
        }

        public override JToken Execute(params JToken[] args)
        {
            switch (args[0].Type)
            {
                
                case JTokenType.Object:
                    return new JValue("object");
                case JTokenType.Array:
                    return new JValue("array");
                case JTokenType.Integer:
                case JTokenType.Float:
                    return new JValue("number");
                case JTokenType.String:
                    return new JValue("string");
                case JTokenType.Boolean:
                    return new JValue("boolean");
                case JTokenType.Null:
                    return new JValue("null");
                
                default:
                    return new JValue("undefined");
            }
        }
    }
}