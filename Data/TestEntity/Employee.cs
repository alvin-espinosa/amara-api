using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace Data.TestEntity
{
    public class Employee : Person
    {
        public List<Dependent> Dependents { get; set; } = new List<Dependent>();
        public Spouse? Spouse { get; set; }

        public static void ConfigureDB(ModelBuilder mb)
        {
            mb.Entity<Employee>()
                .HasMany(a => a.Dependents)
                .WithOne(b => b.Principal)
                .HasForeignKey(a => a.EmployeeId);
        }
    }
}
