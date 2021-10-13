using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TopLogic.Services.Interfaces;
using TopLogicApp.API.DTO;

namespace TopLogicApp.API.Controllers
{
    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        readonly IEmployeeService _employeeService;
        readonly ILogger<EmployeeController> _logger;
        readonly IMapper _mapper;

        public EmployeeController(ILogger<EmployeeController> logger, 
            IEmployeeService employeeService,
            IMapper mapper)
        {
            _logger = logger;
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ResponseCache(NoStore = false,
            Location = ResponseCacheLocation.Client,
            Duration = 60)]
        public async Task<IActionResult> GetEmployees()
        {
            try {
                var result = await _employeeService.GetEmployees();
               
                var dtoEmployees = _mapper.Map<IEnumerable<EmployeeDTO>>(result);
                
                return Ok(dtoEmployees);
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }            
    }
}
