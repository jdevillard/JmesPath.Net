using System;
using DevLab.JmesPath.Expressions;
using Newtonsoft.Json.Linq;
using JmesPathFunction = DevLab.JmesPath.Interop.JmesPathFunction;

namespace DevLab.JmesPath.Functions
{
    public class TypeFunction : JmesPathFunction
    {
        public TypeFunction()
            : base("type", 1)
        {

        }
        public override bool Validate(params JmesPathArgument[] args)
        {
            return true;
        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            switch (args[0].Token.Type)
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