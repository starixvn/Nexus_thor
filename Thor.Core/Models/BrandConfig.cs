namespace Thor.Core.Models
{
    public class BrandConfig
    {
        public string BrandCode { get; set; }

        public bool IsTest { get; set; }

        public bool IsWriteLog { get; set; }

        public string Salt { get; set; }

        public string OperatorCode { get; set; }

        public string ProviderCode { get; set; }

        public string AggregatorCode { get; set; }
    }
}
