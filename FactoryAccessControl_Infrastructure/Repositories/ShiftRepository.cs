

using FactoryAccessControl.Application.InterfaceRepository;
using FactoryAccessControl.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FactoryAccessControl.Infrastructure.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly FactoryDb _context;

        public ShiftRepository(FactoryDb context)
        {
            _context = context;
        }

        public async Task AddAsync(Shift shift)
        {
            await _context.Shifts.AddAsync(shift);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Shift shift)
        {
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync();
        }

        public async Task<Shift?> GetLastShiftForEmployeeAsync(int employeeId)
        {
            return await _context.Shifts
                .Where(s => s.EmployeeId == employeeId)
                .OrderByDescending(s => s.StartTime)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Shift>> GetShiftsByEmployeeIdAsync(int employeeId)
        {
            return await _context.Shifts
                .Where(s => s.EmployeeId == employeeId)
                .OrderByDescending(s => s.StartTime)
                .ToListAsync();
        }
    }

}
