namespace Thor.Core.Models
{
    using System;

    public class GetBetTicketsParams
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public bool Return_Rows { get; set; }

        public string Signature { get; set; }
    }
}
