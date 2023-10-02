using Microsoft.AspNetCore.Identity;
using Training102.BAL.Services.Contract;
using Training102.BAL.SharedModel;
using Training102.DAL;

namespace Training102.BAL.Services.ConcreateImplementation
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel registerViewModel)
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


    }
}
