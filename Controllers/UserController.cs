using hr_app.api.DataContext;
using hr_app.api.Models;
using Microsoft.AspNetCore.Mvc;

namespace hr_app.api.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlManager _sqlManager;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlManager = new MySqlManager(_configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> UserLogin([FromBody] User user)
        {
            int empID = await _sqlManager.LoginUserAsync(user.Username, user.Password);

            if (empID == -1)
            {
                return BadRequest("Invalid username or password");
            }

            return Ok(empID.ToString());
        }
    }
}
