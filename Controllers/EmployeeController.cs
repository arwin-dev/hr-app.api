using hr_app.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.Common;
using hr_app.api.DataContext;

namespace hr_app.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlManager _mySqlManager;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlManager = new MySqlManager(_configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            string query = "SELECT * FROM employee";
            var employees = await _mySqlManager.GetDataTableAsync(query);

            if (employees.Rows.Count == 0)
            {
                return NotFound();
            }

            var serializedData = JsonConvert.SerializeObject(employees);

            return Content(serializedData, "application/json");
        }

        [HttpGet("getEmpDetails")]
        public async Task<IActionResult> GetEmployeeDetails(string empID)
        {
            string query = "SELECT e.First_name, e.Last_name, e.Email, e.Phone_number, j.Title, j.Description FROM employee e, job j WHERE e.Job_ID = j.Job_ID AND e.Employee_ID = @empID";

            var parameters = new Dictionary<string, object>()
            {
                {"@empID", empID }
            };

            var empDetails = await _mySqlManager.GetDataTableAsync(query, parameters);

            if(empDetails.Rows.Count == 0)
            {
                return NotFound();
            }

            var serializedData = JsonConvert.SerializeObject(empDetails);
            return Content(serializedData, "application/json");
        }

 /*       [HttpPost]
        public async Task<IActionResult> AddNewEmployee([FromBody] Employee employee)
        {
            string query = "INSERT INTO employee (Employee_ID, First_name, Last_name, Email, Phone_number, Job_ID) " +
                           "VALUES (@Employee_ID, @First_name, @Last_name, @Email, @Phone_number, @Job_ID)";

            var parameters = new Dictionary<string, object>()
            {
                {"@Employee_ID", employee.Employee_ID},
                {"@First_name", employee.First_Name},
                {"@Last_name", employee.Last_Name},
                {"@Email", employee.Email},
                {"@Phone_number", employee.Phone_number},
                {"@Job_ID", employee.Job_ID}
            };

            var result = await _mySqlManager.ExecuteQueryAsync(query, parameters);

            if (result == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }*/


    }
}
