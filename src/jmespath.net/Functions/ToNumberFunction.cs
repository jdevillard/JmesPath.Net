using System;
using System.Globalization;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class ToNumberFunction : JmesPathFunction
    {
        public ToNumberFunction()
            : base("to_number", 1)
        {

        }
        public override bool Validate(params JToken[] args)
        {
            return true;
        }

        public override JToken Execute(params JToken[] args)
        {
            var arg = args[0];
            if (args[0] == null)
                return null;
            
            switch (arg.Type)
            {
                case JTokenType.Integer:
                case JTokenType.Float:
                    return arg;

                case JTokenType.String:
                    {
                        var value = args[0].Value<string>();

                        int i =0 ;
                        double d = 0;
                        if (int.TryParse(value, out i))
                            return new JValue(i);
                        else if (double.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out d))
                            return new JValue(d);

                        return null;
                    }
                    
                default:
                    return null;
            }
        }
    }
}