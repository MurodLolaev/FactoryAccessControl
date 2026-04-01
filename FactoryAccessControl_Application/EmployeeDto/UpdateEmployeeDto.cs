

using FactoryAccessControl.Domain.Models;

namespace FactoryAccessControl.Application
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; } // номер пропуска, чтобы знать кого обновлять
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public Position Position { get; set; }
    }
}
