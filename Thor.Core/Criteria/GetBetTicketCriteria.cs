namespace Thor.Core.Criteria
{
    using Fanex.Data.Repository;
    using System;

    public class GetBetTicketCriteria : CriteriaBase
    {
        public string BrandCode { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsReturnRows { get; set; }

        public bool IsTest { get; set; }

        public int TotalRows { get; set; }

        public override string GetSettingKey() => "Thor_API_BetTicket_Sel";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(BrandCode) && ToDate >= FromDate;
    }
}
