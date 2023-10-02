using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Training102.BAL.Services.Contract;
using Training102.BAL.SharedModel;
using Training102.DAL;

namespace Training102.BAL.Services.ConcreateImplementation
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManager;
        private IConfiguration _configuration;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public UserService(UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }


        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel registerViewModel ,string role ="User")
        {

            if (registerViewModel == null)
                throw new ArgumentNullException(nameof(registerViewModel));

            User identityUser = new User
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.UserName,
            };
            if (registerViewModel.Password != registerViewModel.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = true,
                    Message = "Confirm password doesn't match the password",
                };

            }
            var response = await _userManager.CreateAsync(identityUser, registerViewModel.Password);
            var roleResult = await _userManager.AddToRoleAsync(identityUser, role);

            if (response.Succeeded)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = true,
                    Message = "User created successfully"

                };
            }
            return new UserManagerResponse
            {
                IsSuccessful = false,
                Message = "User did not created",
                Errors = response.Errors.Select(c => c.Description)
            };


        }
        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailOrUserName);


            var checkPassword = await _userManager.CheckPasswordAsync(user?? new User(), loginViewModel.Password);
            
            if (user != null && checkPassword != true)
            {
                return UserInvalidMessage();

            }
         

            var claims = new[]
            {
             new Claim(ClaimTypes.Email,user.Email),
             new Claim(ClaimTypes.NameIdentifier , user.UserName),
             new Claim(ClaimTypes.Role , "Admin")
            };

            var keyBuffer = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration["AuthSetting:SymmetricSecurityKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSetting:ValidIssuer"],
                audience: _configuration["AuthSetting:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: new SigningCredentials(keyBuffer, 
                SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return new UserManagerResponse
            {
                IsSuccessful = true,
                Message = tokenAsString,
                ExprieDate = token.ValidTo,
                Username = user.UserName,
            };
         
        }

        private static UserManagerResponse UserInvalidMessage()
        {
            return new UserManagerResponse
            {
                IsSuccessful = false,
                Message = "Invalid credentials",

            };
        }
    }
}
