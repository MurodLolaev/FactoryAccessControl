using FactoryAccessControl.Application.InterfaceService;
using FactoryAccessControl.Application;
using FactoryAccessControl.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FactoryAccessControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // Добавить сотрудника
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateEmployeeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var employee = await _employeeService.CreateEmployeeAsync(dto);
            if (employee == null) return BadRequest("Ошибка при добавлении сотрудника");

            return Ok(employee);
        }

        // Изменить сотрудника
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var employee = await _employeeService.UpdateEmployeeAsync(dto);
            if (employee == null) return BadRequest("Сотрудник не найден");

            return Ok(employee);
        }

        // Удалить сотрудника
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            if (!result) return BadRequest("Сотрудник не найден");

            return Ok("Сотрудник удален");
        }

        // Получить всех сотрудников, опционально по должности
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] Position? position = null)
        {
            var employees = await _employeeService.GetAllEmployeesAsync(position);
            return Ok(employees);
        }

        // Получить список всех должностей
        [HttpGet("positions")]
        public IActionResult GetPositions()
        {
            var positions = Enum.GetValues(typeof(Position));
            return Ok(positions);
        }

        // Получение количествие всех замечание течение месяц в определёный год 
        [HttpGet("violations")]
        public async Task<IActionResult> GetMonthlyViolations([FromQuery] int year, [FromQuery] int month)
        {
            var stats = await _employeeService.GetMonthlyViolationsAsync(year, month);
            return Ok(stats);
        }
    }
}
