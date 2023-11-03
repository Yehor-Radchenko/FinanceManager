using FinanceManagerAPI.Data;
using FinanceManagerAPI.Models;
using FinanceManagerAPI.Services;
using FinanceManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : Controller
    {
        private readonly IFinancialOperationService _operationService;
        public OperationController(IFinancialOperationService service)
        {
            _operationService = service;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OperationDto>))]
        public async Task<IActionResult> GetOperations()
        {
            return Ok(await _operationService.GetAll());
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(FinancialOperation))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOperation(int Id)
        {
            var operation = await _operationService.GetById(Id);
            if (operation is null)
                return NotFound();

            return Ok(operation);
        }

        [HttpPost]
        public async Task<IActionResult> PostOperation([FromBody] OperationDto operation)
        {
            await _operationService.Create(operation);
            return Ok("Successfully created");
        }

        [HttpPut]
        public async Task<IActionResult> PutOperation(OperationDto operation)
        {
            await _operationService.Update(operation);
            return Ok("Updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperation(int id)
        {
            await _operationService.Delete(id);
            return Ok("Deleted successfully.");
        }

        [HttpGet("daily-report")]
        public async Task<IActionResult> GetDailyReport(string date)
        {
            var report = await _operationService.GetOperationsForPeriod(date);
            return Ok(report);
        }

        [HttpGet("period-report")]
        public async Task<IActionResult> GetPeriodReport(string startDate, string endDate)
        {
            var report = await _operationService.GetOperationsForPeriod(startDate, endDate);
            return Ok(report);
        }
    }
}
