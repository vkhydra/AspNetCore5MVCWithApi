using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TesteWebApp.Models;

namespace TesteWebApp.Controllers.Api
{
    [ApiController]
    [Area("api")]
    [Route("[area]/[controller]")]
    public class UserController : ControllerBase
    {
        public UserManager<AppUserModel> UserManager { get; set; }
        public UserController(UserManager<AppUserModel> userManager)
        {
            UserManager = userManager;
        }

        [HttpPost("email")]
        public async Task<IActionResult> GetEmailByUserName([FromBody] string userName)
        {
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.Email);
        }
        [HttpPost("role")]
        public async Task<IActionResult> GetRoleByUserName([FromBody] string userName)
        {
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }
            var roles = (await UserManager.GetRolesAsync(user)).ToList();
            if (roles == null || roles.Count == 0)
            {
                return NotFound();
            }
            return Ok(roles);
        }
    }
}
