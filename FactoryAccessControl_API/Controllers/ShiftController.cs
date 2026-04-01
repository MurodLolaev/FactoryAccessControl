using FactoryAccessControl.Application.InterfaceService;
using FactoryAccessControl.Application;
using Microsoft.AspNetCore.Mvc;

namespace FactoryAccessControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        // Начало смены
        [HttpPost("start")]
        public async Task<IActionResult> StartShift([FromBody] StartShiftDto dto)
        {
            var shift = await _shiftService.StartShiftAsync(dto);
            if (shift == null) return BadRequest("Нельзя начать смену: предыдущая еще не закрыта или сотрудник не найден");

            return Ok(shift);
        }

        // Конец смены
        [HttpPost("end")]
        public async Task<IActionResult> EndShift([FromBody] EndShiftDto dto)
        {
            var shift = await _shiftService.EndShiftAsync(dto);
            if (shift == null) return BadRequest("Нельзя закрыть смену: смена не найдена или уже закрыта");

            return Ok(shift);
        }

        // Получить все смены сотрудника
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetShifts(int employeeId)
        {
            var shifts = await _shiftService.GetShiftsForEmployeeAsync(employeeId);
            return Ok(shifts);
        }
    }
}
