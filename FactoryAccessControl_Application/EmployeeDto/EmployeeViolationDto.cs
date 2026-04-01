

using FactoryAccessControl.Domain.Models;

namespace FactoryAccessControl.Application
{
    public class EmployeeViolationDto
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null!;
        public Position Position { get; set; }
        public int ViolationsCount { get; set; }
    }
}
