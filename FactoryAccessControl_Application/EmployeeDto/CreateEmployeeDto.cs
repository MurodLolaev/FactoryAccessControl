

using FactoryAccessControl.Domain.Models;

namespace FactoryAccessControl.Application
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public Position Position { get; set; }
    }
}
