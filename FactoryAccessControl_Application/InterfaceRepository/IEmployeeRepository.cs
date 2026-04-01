

using FactoryAccessControl.Domain.Models;

namespace FactoryAccessControl.Application.InterfaceRepository
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<Employee?> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync(Position? position = null);
    }
}
