

using FactoryAccessControl.Domain.Models;

namespace FactoryAccessControl.Application
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public Position Position { get; set; }
    }
}
