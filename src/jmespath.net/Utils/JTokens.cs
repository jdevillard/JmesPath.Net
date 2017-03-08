using System;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Utils
{
    public static class JTokens
    {
        public static JToken Null = JToken.Parse("null");
        public static JToken True = JToken.Parse("true");
        public static JToken False = JToken.Parse("false");

        public static bool IsFalse(JToken token)
        {
            // A false value corresponds to any of the following conditions:
            // Empty list: ``[]``
            // Empty object: ``{}``
            // Empty string: ``""``
            // False boolean: ``false``
            // Null value: ``null``

            var array = token as JArray;
            if (array != null && array.Count == 0)
                return true;

            var @object = token as JObject;
            if (@object != null && @object.Count == 0)
                return true;

            var value = token as JValue;
            if (value != null)
            {
                switch (token.Type)
                {
                    case JTokenType.Bytes:
                    case JTokenType.Date:
                    case JTokenType.Guid:
                    case JTokenType.String:
                        return token.Value<String>() == "";

                    case JTokenType.Boolean:
                        return token.Value<Boolean>() == false;

                    case JTokenType.Null:
                        return true;
                }
            }

            return false;
        }
    }
}