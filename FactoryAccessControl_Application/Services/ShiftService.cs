

using FactoryAccessControl.Application.InterfaceRepository;
using FactoryAccessControl.Application.InterfaceService;
using FactoryAccessControl.Application;
using FactoryAccessControl.Domain.Models;
namespace FactoryAccessControl.Application.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;

        public ShiftService(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public async Task<ShiftDto?> StartShiftAsync(StartShiftDto dto)
        {
            // Проверяем, есть ли незакрытая смена
            var lastShift = await _shiftRepository.GetLastShiftForEmployeeAsync(dto.EmployeeId);
            if (lastShift != null && lastShift.EndTime == null)
                return null; // нельзя начать новую смену пока старая не закрыта

            var shift = new Shift
            {
                EmployeeId = dto.EmployeeId,
                StartTime = dto.StartTime
            };

            await _shiftRepository.AddAsync(shift);

            return new ShiftDto
            {
                Id = shift.Id,
                EmployeeId = shift.EmployeeId,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                WorkedHours = shift.WorkedHours
            };
        }

        public async Task<ShiftDto?> EndShiftAsync(EndShiftDto dto)
        {
            var lastShift = await _shiftRepository.GetLastShiftForEmployeeAsync(dto.EmployeeId);
            if (lastShift == null || lastShift.EndTime != null)
                return null; // нельзя закрыть несуществующую смену

            lastShift.EndTime = dto.EndTime;
            lastShift.WorkedHours = (lastShift.EndTime - lastShift.StartTime)?.TotalHours;

            await _shiftRepository.UpdateAsync(lastShift);

            return new ShiftDto
            {
                Id = lastShift.Id,
                EmployeeId = lastShift.EmployeeId,
                StartTime = lastShift.StartTime,
                EndTime = lastShift.EndTime,
                WorkedHours = lastShift.WorkedHours
            };
        }

        public async Task<IEnumerable<ShiftDto>> GetShiftsForEmployeeAsync(int employeeId)
        {
            var shifts = await _shiftRepository.GetShiftsByEmployeeIdAsync(employeeId);

            return shifts.Select(s => new ShiftDto
            {
                Id = s.Id,
                EmployeeId = s.EmployeeId,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                WorkedHours = s.WorkedHours
            });
        }
    }
}
