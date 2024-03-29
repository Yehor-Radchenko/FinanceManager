﻿using FinanceManagerCommon.Data;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<OperationUpdateDto>))]
        public async Task<IActionResult> GetOperations()
        {
            return Ok(await _operationService.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(FinancialOperation))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOperation([FromRoute] int id)
        {
            var operation = await _operationService.GetById(id);
            return Ok(operation);
        }

        [HttpPost]
        public async Task<IActionResult> PostOperation([FromBody] OperationCreateDto operation)
        {
            if(await _operationService.Create(operation))
                return Ok("Successfully created");
            else return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> PutOperation([FromBody] OperationUpdateDto operation)
        {
            if(await _operationService.Update(operation))
                return Ok("Updated successfully.");
            else return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperation([FromRoute] int id)
        {
            if (await _operationService.Delete(id))
                return Ok("Deleted successfully.");
            else return BadRequest();
        }
    }
}
