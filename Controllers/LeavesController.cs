using hr_app.api.DataContext;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace hr_app.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeavesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlManager _mySqlManager;

        public LeavesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlManager = new MySqlManager(_configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaves(string empID)
        {
            string query = "SELECT l.Leave_ID, l.Number_of_days, l.Reason, j.PTO FROM employee e, leaves l, job j WHERE e.Employee_ID = l.Employee_ID AND e.Job_ID = j.Job_ID AND e.Employee_ID = @empID";
            var parameters = new Dictionary<string, object>
            {
                { "@empID", empID }
            };
            var leaves = await _mySqlManager.GetDataTableAsync(query, parameters);



            var serializedData = JsonConvert.SerializeObject(leaves);

            return Content(serializedData, "application/json");
        }

        [HttpGet("getPto")]
        public async Task<IActionResult> GetPto(string empID)
        {
            string query = "SELECT  j.PTO FROM employee e, job j WHERE e.Job_ID = j.Job_ID AND e.Employee_ID = @empID";
            var parameters = new Dictionary<string, object>
            {
                { "@empID", empID }
            };
            var leaves = await _mySqlManager.GetDataTableAsync(query, parameters);



            var serializedData = JsonConvert.SerializeObject(leaves);

            return Content(serializedData, "application/json");
        }

        [HttpPost("addleaves")]
        public async Task<IActionResult> AddLeaves(Dictionary<string, object> leavesDict)
        {
            string query = "INSERT INTO leaves (Number_of_days, Reason, Employee_ID)" +
                            "VALUES (@Number_of_days,@Reason,@empID)";
            var parameters = new Dictionary<string, object>
            {
                {"@Number_of_days", leavesDict["Number_of_days"] },
                {"@Reason", leavesDict["Reason"] },
                {"@empID", leavesDict["empID"] }
            };
            
            var result = await _mySqlManager.ExecuteQueryAsync(query, parameters);
            if(result == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
