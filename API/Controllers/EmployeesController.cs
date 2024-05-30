using Data;
using Data.TestEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly RentingContext rentingContext;

        public EmployeesController(RentingContext rentingContext)
        {
            this.rentingContext = rentingContext;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            var employees = await rentingContext.Employees
                .Include(_ => _.Dependents)
                .Include(_ => _.Spouse)
                .ToListAsync();

            var dependets = rentingContext.Dependents.Include(_ => _.Principal).ThenInclude(_ => _.Spouse)
                .ToList();

            return Ok(employees);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> AddEmployeeAsync()
        {
            var employee = new Employee { Name = "Alvin Espinosa"};
            employee.Dependents.AddRange(new List<Dependent> {
                new Dependent {Name = "Amara Elein Espinosa", Birthdate = new DateOnly(2009, 8, 16)},
                new Dependent {Name = "Kevin Andrei Espinosa", Birthdate = new DateOnly(2014, 1, 7)},
            });

            employee.Spouse = new Spouse { Name = "Kresleen Cara", Birthdate = new DateOnly(1987, 1, 22) };
            rentingContext.Employees.Add(employee);

            await rentingContext.SaveChangesAsync();

            return Ok();
        }
    }
}
