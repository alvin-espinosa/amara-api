using Microsoft.EntityFrameworkCore;

namespace Data.TestEntity
{
    public class Dependent: Person
    {
        public Employee Principal { get; set; } = new Employee();
        public Guid EmployeeId { get; set; }

        public static void ConfigureDB(ModelBuilder mb)
        {
            
        }
    }
}
