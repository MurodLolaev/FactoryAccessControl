

using FactoryAccessControl.Application;

namespace FactoryAccessControl.Application.InterfaceService
{
    public interface IShiftService
    {
        Task<ShiftDto?> StartShiftAsync(StartShiftDto dto);
        Task<ShiftDto?> EndShiftAsync(EndShiftDto dto);
        Task<IEnumerable<ShiftDto>> GetShiftsForEmployeeAsync(int employeeId);
    }
}
