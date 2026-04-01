
namespace FactoryAccessControl.Application
{
    public class StartShiftDto
    {
        public int EmployeeId { get; set; } // номер пропуска сотрудника
        public DateTime StartTime { get; set; } // время прихода
    }
}
