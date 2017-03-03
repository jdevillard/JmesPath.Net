using System;
using Newtonsoft.Json.Linq;

namespace jmespath.net.compliance
{
    public sealed class ComplianceResult
    {
        public Boolean Success { get; set; }
        public String Error { get; set; }
        public JToken Result { get; set; }
    }
}