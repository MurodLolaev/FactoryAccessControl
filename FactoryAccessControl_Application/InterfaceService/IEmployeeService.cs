

using FactoryAccessControl.Application;
using FactoryAccessControl.Domain.Models;

namespace FactoryAccessControl.Application.InterfaceService
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto);
        Task<EmployeeDto?> UpdateEmployeeAsync(UpdateEmployeeDto dto);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(Position? position = null);
        Task<IEnumerable<Position>> GetAllPositionsAsync();

        //  метод для бонуса
        Task<IEnumerable<EmployeeViolationDto>> GetMonthlyViolationsAsync(int year, int month);
    }
}
