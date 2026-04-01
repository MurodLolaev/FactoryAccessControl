

namespace FactoryAccessControl.Application
{
    public class ShiftDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double? WorkedHours { get; set; }
    }
}
