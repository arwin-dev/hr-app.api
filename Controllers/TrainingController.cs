using hr_app.api.DataContext;
using hr_app.api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hr_app.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlManager _sqlManager;

        public TrainingController(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlManager = new MySqlManager(_configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTraining(string empID)
        {
            string query = "SELECT t.Name, t.Mode, et.Start_date, et.Completion_date, et.Score FROM training t, employee_training et WHERE t.Training_ID = et.Training_ID AND et.Employee_ID = @empID";
            var parameters = new Dictionary<string, object> 
            { 
                { "@empID", empID } 
            };
            var trainings = await _sqlManager.GetDataTableAsync(query, parameters);

            if (trainings.Rows.Count == 0)
            {
                return NotFound();
            }

            var serializedData = JsonConvert.SerializeObject(trainings);

            return Content(serializedData, "application/json");
        }

/*        [HttpPost]
        public async Task<IActionResult> AddTraining([FromBody] Training training)
        {

        }*/
    }
}
