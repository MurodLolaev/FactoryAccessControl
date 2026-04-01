

using FactoryAccessControl.Application;
using FactoryAccessControl.Application.InterfaceRepository;
using FactoryAccessControl.Application.InterfaceService;
using FactoryAccessControl.Domain.Models;

namespace FactoryAccessControl.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IShiftRepository _shiftRepository;

        public EmployeeService(IEmployeeRepository repository, IShiftRepository shiftRepository)
        {
            _repository = repository;
            _shiftRepository = shiftRepository;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                Position = dto.Position
            };

            await _repository.AddAsync(employee);

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Position = employee.Position
            };
        }

        public async Task<EmployeeDto?> UpdateEmployeeAsync(UpdateEmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(dto.Id);
            if (employee == null) return null;

            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.MiddleName = dto.MiddleName;
            employee.Position = dto.Position;

            await _repository.UpdateAsync(employee);

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Position = employee.Position
            };
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) return false;

            await _repository.DeleteAsync(employee);
            return true;
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Position = employee.Position
            };
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(Position? position = null)
        {
            var employees = await _repository.GetAllAsync(position);

            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                MiddleName = e.MiddleName,
                Position = e.Position
            });
        }

        public Task<IEnumerable<Position>> GetAllPositionsAsync()
        {
            var positions = Enum.GetValues(typeof(Position)).Cast<Position>();
            return Task.FromResult(positions);
        }

        public async Task<IEnumerable<EmployeeViolationDto>> GetMonthlyViolationsAsync(int year, int month)
        {
            var employees = await _repository.GetAllAsync();

            var violations = new List<EmployeeViolationDto>();

            foreach (var emp in employees)
            {
                // Получаем смены сотрудника за месяц
                var shifts = await _shiftRepository.GetShiftsByEmployeeIdAsync(emp.Id);
                var monthShifts = shifts
                    .Where(s => s.StartTime.Year == year && s.StartTime.Month == month);

                int count = 0;

                foreach (var shift in monthShifts)
                {
                    if (emp.Position == Position.Tester) // тестировщик свечей
                    {
                        if (shift.StartTime.Hour > 9 || (shift.EndTime?.Hour < 21)) count++;
                    }
                    else // обычные сотрудники
                    {
                        if (shift.StartTime.Hour > 9 || (shift.EndTime?.Hour < 18)) count++;
                    }
                }

                violations.Add(new EmployeeViolationDto
                {
                    EmployeeId = emp.Id,
                    FullName = $"{emp.LastName} {emp.FirstName} {emp.MiddleName}",
                    Position = emp.Position,
                    ViolationsCount = count
                });
            }

            return violations;
        }
    }
}
