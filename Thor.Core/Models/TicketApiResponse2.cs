namespace Thor.Core.Models
{
    using System.Collections.Generic;

    public class TicketApiResponse2
    {
        public int success { get; set; }

        public string provider_code { get; set; }

        public string operator_code { get; set; }

        public string aggregator_code { get; set; }

        public int result_count { get; set; }

        public int page_count { get; set; }

        public IEnumerable<BetTicket2> game_transactions { get; set; }

    }
}
