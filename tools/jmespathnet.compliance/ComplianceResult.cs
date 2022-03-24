using Newtonsoft.Json.Linq;

namespace jmespath.net.compliance
{
    public sealed class ComplianceResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public JToken Result { get; set; }
    }
}