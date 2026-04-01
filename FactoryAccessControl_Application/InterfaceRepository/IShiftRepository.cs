
using FactoryAccessControl.Domain.Models;

namespace FactoryAccessControl.Application.InterfaceRepository
{
    public interface IShiftRepository
    {
        Task AddAsync(Shift shift);
        Task UpdateAsync(Shift shift);
        Task<Shift?> GetLastShiftForEmployeeAsync(int employeeId);
        Task<IEnumerable<Shift>> GetShiftsByEmployeeIdAsync(int employeeId);
    }
}
