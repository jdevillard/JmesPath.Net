using Newtonsoft.Json;
using System.Collections.Generic;

namespace jmespath.net.compliance
{
    public sealed class ComplianceReport
    {
        [JsonProperty("compliance")]
        public double Compliance { get; set; }

        [JsonProperty("functionSets")]
        public FunctionSet[] FunctionSets { get; set; }
    }

    public sealed class FunctionSet {

        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("compliance")]
        public double Compliance { get; set; }
    }
}