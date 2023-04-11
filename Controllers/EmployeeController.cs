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

    }
}
