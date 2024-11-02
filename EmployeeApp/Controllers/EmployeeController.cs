using EmployeeApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Controllers
{
    [ApiController]
    [Route("employeeApi")]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }



        [HttpPost(Name = "InsertEmployee")]
        public async Task<IActionResult> InsertEmployee(Employee employee)
        {
            var parameters = new[] {

                new SqlParameter("@Id",DBNull.Value),
                new SqlParameter("@FName",employee.fName ?? (object)DBNull.Value),
                new SqlParameter("@LName",employee.lName?? (object)DBNull.Value),
                new SqlParameter("@age",employee.age?? (object)DBNull.Value),
                new SqlParameter("@salary",employee.salary?? (object)DBNull.Value),
                new SqlParameter("@country",employee.country?? (object)DBNull.Value),
                new SqlParameter("@phone",employee.phone?? (object)DBNull.Value),
                new SqlParameter("@gender",employee.gender?? (object)DBNull.Value),
                new SqlParameter("@email",employee.email?? (object)DBNull.Value),
                new SqlParameter("@state",employee.state?? (object)DBNull.Value)

            };

            await _context.Database.ExecuteSqlRawAsync("EXEC SaveOrUpdateEmployee @Id,@FName,@LName,@age,@salary,@country,@phone,@gender,@email,@state ",
                parameters);
            return Ok(employee);
        }

        [HttpPut(Name = "UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            var parameters = new[] {

                new SqlParameter("@Id",id),
                new SqlParameter("@FName",employee.fName ?? (object)DBNull.Value),
                new SqlParameter("@LName",employee.lName?? (object)DBNull.Value),
                new SqlParameter("@age",employee.age?? (object)DBNull.Value),
                new SqlParameter("@salary",employee.salary?? (object)DBNull.Value),
                new SqlParameter("@country",employee.country?? (object)DBNull.Value),
                new SqlParameter("@phone",employee.phone?? (object)DBNull.Value),
                new SqlParameter("@gender",employee.gender?? (object)DBNull.Value),
                new SqlParameter("@email",employee.email?? (object)DBNull.Value),
                new SqlParameter("@state",employee.state?? (object)DBNull.Value)

            };

            var user = _context.Employees.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC SaveOrUpdateEmployee @Id,@FName,@LName,@age,@salary,@country,@phone,@gender,@email,@state ",
                    parameters);
                return Ok(employee);
            }
        }


        [HttpDelete(Name = "DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var parameters = new[] {

                new SqlParameter("@Id",id),


            };
            var user = _context.Employees.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC DeleteEmployee @id", parameters);
                return Ok();
            }
        }


    }
}
