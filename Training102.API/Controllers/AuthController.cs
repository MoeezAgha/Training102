using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Training102.BAL.Services.Contract;
using Training102.BAL.SharedModel;
using Training102.DAL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Training102.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        private readonly IConfiguration _configuration;
        private IUserService _userService;

        public AuthController(UserManager<User> userManager, IConfiguration configuration, IUserService userService, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _userService = userService;
         _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
               var roleName= await  _roleManager.FindByNameAsync("User");

                var result = await _userService.RegisterUserAsync(model, roleName.Name);

                if (result.IsSuccessful)
                {
                    // Assuming you have a role called "User" (you can replace it with your desired role name)

                        // Assign the "User" role to the newly registered user
                        var user = await _userManager.FindByNameAsync(model.UserName);
                        await _userManager.AddToRoleAsync(user, "User");
                        return Ok(result);
                    
                }

                return BadRequest(new { Message = "Invalid registration data", Errors = ModelState.Values });
            }
            return Ok (ModelState.Select(c=>c.Value));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.IsSuccessful)
                    return Ok(result);
                return BadRequest(result);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
           
            return BadRequest(errors);
         
        }
    }
}
