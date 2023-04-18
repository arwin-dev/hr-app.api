using hr_app.api.DataContext;
using hr_app.api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace hr_app.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlManager _mySqlManager;

        public JobsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlManager = new MySqlManager(_configuration.GetConnectionString("DefaultConnection"));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            string query = "SELECT * FROM job";
            var jobs = await _mySqlManager.GetDataTableAsync(query);

            if (jobs.Rows.Count == 0)
            {
                return NotFound();
            }

            var serializedData = JsonConvert.SerializeObject(jobs);

            return Content(serializedData, "application/json");
        }
    }
}
