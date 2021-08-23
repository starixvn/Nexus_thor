namespace Thor.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using ONEbook.UM.Services;
    using Thor.Core.Constants;
    using Thor.Core.Criteria;
    using Thor.Core.Models;
    using Thor.Core.Services;


    [ApiController]
    [Route("api/Ticket")]
    public class TicketController : ControllerBase
    {
        private readonly IMaintenanceService maintenanceService;
        private readonly IBaseService baseService;
        private readonly IOptionsSnapshot<BrandConfig> brandConfig;

        private readonly ILogServiceExtend logService;

        public TicketController(IMaintenanceService maintenanceService,
            IBaseService baseService,
            IOptionsSnapshot<BrandConfig> brandConfig,
            ILogServiceExtend logService)
        {
            this.maintenanceService = maintenanceService;
            this.baseService = baseService;
            this.brandConfig = brandConfig;
            this.logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DateTime from, DateTime to, bool return_rows, string signature)
        {
            if (brandConfig.Value.IsWriteLog)
            {
                logService.WriteLogFileInfo(
                    string.Format("Params: form-{0}, to-{1}, return_rows-{2}, signature-{3}", 
                    from, to, return_rows, signature));
            }

            if (!signature.Equals(brandConfig.Value.Salt, StringComparison.OrdinalIgnoreCase))
            {
                logService.WriteLogFileInfo(string.Format("Unauthorized-{0}", signature));
                return Unauthorized();
            }

            TicketApiResponse ticketApiResponse;
            if ((to-from).TotalDays > 180)
            {
                ticketApiResponse = GetResponseWhenInvalidParams();
            }
            else
            {
                try
                {
                    if (IsUM())
                    {
                        ticketApiResponse = GetUnderMaintainanceResponse();
                    }
                    else
                    {
                        ticketApiResponse = await GetSuccessTicketApiResponse(from, to, return_rows);
                    }
                }
                catch (Exception ex)
                {
                    ticketApiResponse = GetInternalServerErrorResponse();

                    logService.WriteLogFileError(ex.ToString());
                    await logService.WriteExceptionSentryErrorAsync(ex);
                }
            }

            ticketApiResponse = AppendBrandInfo(ticketApiResponse);

            return Ok(ticketApiResponse);
        }

        private TicketApiResponse AppendBrandInfo(TicketApiResponse ticketResponse)
        {
            ticketResponse.operator_code = brandConfig.Value.OperatorCode;
            ticketResponse.provider_code = brandConfig.Value.ProviderCode;
            ticketResponse.aggregator_code = brandConfig.Value.AggregatorCode;
            return ticketResponse;
        }

        private TicketApiResponse GetResponseWhenInvalidParams()
        {
            var ticketResponse = new TicketApiResponse
            {
                status = Status.Fail,
                message = BrandMessages.GetExceedDateRangeMessage(brandConfig.Value.BrandCode),
                total_rows = 0,
                data = new List<BetTicket>()
            };

            return ticketResponse;
        }

        private TicketApiResponse GetInternalServerErrorResponse()
        {
            var ticketResponse = new TicketApiResponse
            {
                status = Status.Fail,
                message = BrandMessages.GetInternalErrorMessage(brandConfig.Value.BrandCode),
                total_rows = 0,
                data = new List<BetTicket>()
            };

            return ticketResponse;
        }

        private TicketApiResponse GetUnderMaintainanceResponse()
        {
            var ticketResponse = new TicketApiResponse
            {
                status = Status.Fail,
                message = BrandMessages.GetUnderMaintenanceMessage(brandConfig.Value.BrandCode),
                total_rows = 0,
                data = new List<BetTicket>()
            };

            return ticketResponse;
        }

        private async Task<TicketApiResponse> GetSuccessTicketApiResponse(DateTime from, DateTime to, bool return_rows)
        {
            TicketApiResponse betTicketResponse;
            var criteria = new GetBetTicketCriteria
            {
                BrandCode = brandConfig.Value.BrandCode,
                FromDate = from,
                ToDate = to,
                IsReturnRows = return_rows,
                IsTest = brandConfig.Value.IsTest,
            };

            var betTickets = await baseService.GetDataAsync<BetTicket>(criteria);
            if (criteria.IsReturnRows)
            {
                betTicketResponse = new TicketApiResponse
                {
                    status = Status.Success,
                    message = string.Empty,
                    total_rows = criteria.TotalRows,
                    data = betTickets
                };
            }
            else
            {
                betTicketResponse = new TicketApiResponse
                {
                    status = Status.Success,
                    message = string.Empty,
                    total_rows = criteria.TotalRows,
                    data = new List<BetTicket>()
                };
            }

            if (brandConfig.Value.IsWriteLog)
            {
                logService.WriteLogFileInfo(string.Format("TotalRows: {0}", criteria.TotalRows));
            }

            return betTicketResponse;
        }

        private bool IsUM()
        {
            var maintanceTime = maintenanceService.GetUnderMaintenanceTime();
            return maintanceTime.IsUnderMaintenanceTime;
        }
    }
}
