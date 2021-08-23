namespace Thor.Core.Models
{
    using System.Collections.Generic;

    public class TicketApiResponse
    {
        public string status { get; set; }

        public string message { get; set; }

        public int total_rows { get; set; }

        public string provider_code { get; set; }

        public string operator_code { get; set; }

        public string aggregator_code { get; set; }

        public IEnumerable<BetTicket> data { get; set; }

    }
}
