

namespace FactoryAccessControl.Domain.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double? WorkedHours { get; set; }
        public int EmployeeId { get; set; } // внешний ключ
    }
}
