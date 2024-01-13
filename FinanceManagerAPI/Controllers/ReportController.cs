using FinanceManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService service)
        {
            _reportService = service;
        }

        [HttpGet("daily-report")]
        public async Task<IActionResult> GetDailyReport([FromQuery] string date)
        {
            var report = await _reportService.GetOperationsForPeriod(date);
            return Ok(report);
        }

        [HttpGet("period-report")]
        public async Task<IActionResult> GetPeriodReport([FromQuery] string startDate, [FromQuery] string endDate)
        {
            var report = await _reportService.GetOperationsForPeriod(startDate, endDate);
            return Ok(report);
        }
    }
}
