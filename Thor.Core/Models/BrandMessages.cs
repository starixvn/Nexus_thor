namespace Thor.Core.Models
{
    using Thor.Core.Constants;

    public static class BrandMessages
    {
        public const string InternalErrorNova88 = "Internal server error";
        public const string ExceedDateRangeNova88 = "Date range is too large";
        public const string UnderMaintenanceNova88 = "System is under maintenance";
        
        public const string InternalError12Bet = "Server error";
        public const string ExceedDateRange12Bet = "Date range exceed limit";
        public const string UnderMaintenance12Bet = "The whole system is under maintenance";

        public static string GetInternalErrorMessage(string brandCode)
        {
            string errorMessage;
            switch (brandCode)
            {
                case BrandCodes.Nova88:
                    errorMessage = InternalErrorNova88;
                    break;
                case BrandCodes._12Bet:
                    errorMessage = InternalError12Bet;
                    break;
                default:
                    errorMessage = InternalErrorNova88;
                    break;
            }

            return errorMessage;
        }

        public static string GetExceedDateRangeMessage(string brandCode)
        {
            string errorMessage;
            switch (brandCode)
            {
                case BrandCodes.Nova88:
                    errorMessage = ExceedDateRangeNova88;
                    break;
                case BrandCodes._12Bet:
                    errorMessage = ExceedDateRange12Bet;
                    break;
                default:
                    errorMessage = ExceedDateRangeNova88;
                    break;
            }

            return errorMessage;
        }

        public static string GetUnderMaintenanceMessage(string brandCode)
        {
            string errorMessage;
            switch (brandCode)
            {
                case BrandCodes.Nova88:
                    errorMessage = UnderMaintenanceNova88;
                    break;
                case BrandCodes._12Bet:
                    errorMessage = UnderMaintenance12Bet;
                    break;
                default:
                    errorMessage = UnderMaintenanceNova88;
                    break;
            }

            return errorMessage;
        }
    }
}
