

namespace FactoryAccessControl.Domain.Models
{
    public class Employee
    {
        public int Id { get; set; } // номер пропуска
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public Position Position { get; set; }
    }
}
