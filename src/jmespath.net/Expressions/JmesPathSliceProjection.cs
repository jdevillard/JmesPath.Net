using System;
using System.Collections.Generic;
using System.Linq;
using DevLab.JmesPath.Interop;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Expressions
{
    /// <summary>
    /// Represents a JmesPath slice expression.
    /// </summary>
    public class JmesPathSliceProjection : JmesPathProjection
    {
        private readonly int? start_;
        private readonly int? stop_;
        private readonly int? step_;

        public JmesPathSliceProjection(int? start, int? stop, int? step)
        {
            start_ = start;
            stop_ = stop;
            step_ = step;
        }

        public int? Start
            => start_;

        public int? Stop
            => stop_;

        public int? Step
            => step_;

        public override JmesPathArgument Project(JmesPathArgument argument)
        {
            if (argument.IsProjection)
                argument = argument.AsJToken();

            var json = argument.Token;

            if (json.Type != JTokenType.Array)
                return null;

            // slice expression adhere to the following rule:
            // if the element being sliced is not an array, the result is null.

            var array = json as JArray;
            if (array == null)
                return null;

            // slice expressions adhere to the following rules:
            // if the given step is omitted, it it assumed to be 1.

            var step = step_ ?? 1;

            // if the given step is 0, an error MUST be raised.
            // no runtime check here - the parser will ensure that 0 is not a valid value

            System.Diagnostics.Debug.Assert(step != 0);

            var length = array.Count;

            // if no start position is given, it is assumed to be 0 if the given step is greater than 0 or the end of the array if the given step is less than 0.

            var start = start_ ?? (step > 0 ? 0 : length - 1);

            // if a negative start position is given, it is calculated as the total length of the array plus the given start position.

            if (start_.HasValue && start_.Value < 0)
                start = length + start_.Value;

            // if no stop position is given, it is assumed to be the length of the array if the given step is greater than 0 or 0 if the given step is less than 0.

            var stop = stop_ ?? (step > 0 ? length : -1);

            // if a negative stop position is given, it is calculated as the total length of the array plus the given stop position.

            if (stop_.HasValue && stop_.Value < 0)
                stop = length + stop_.Value;

            // if the element being sliced is an array and yields no results, the result MUST be an empty array.

            var items = new List<JToken>();

            for (var index = start; (step > 0 ? index < stop : index > stop); index += step)
                if (index >= 0 && index < length)
                    items.Add(array[index]);

            var arguments = items.Select(i => (JmesPathArgument)i);

            return new JmesPathArgument(arguments);
        }
    }
}