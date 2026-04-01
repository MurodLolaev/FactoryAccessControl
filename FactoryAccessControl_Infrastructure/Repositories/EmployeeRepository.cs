

using FactoryAccessControl.Application.InterfaceRepository;
using FactoryAccessControl.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace FactoryAccessControl.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly FactoryDb _context;

        public EmployeeRepository(FactoryDb context)
        {
            _context = context;
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(Position? position = null)
        {
            var query = _context.Employees.AsQueryable();
            if (position.HasValue)
                query = query.Where(e => e.Position == position.Value);

            return await query.ToListAsync();
        }
    }
}
