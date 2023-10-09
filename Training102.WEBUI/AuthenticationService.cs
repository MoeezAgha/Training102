using System.ComponentModel.DataAnnotations;

namespace Training102.WEBUI
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public class LoginViewModel
        {


            [Required(ErrorMessage = "Please enter your username or email.")]
            [Display(Name = "Username or Email")]
            public string EmailOrUserName { get; init; }

            [Required(ErrorMessage = "Please enter your password.")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; init; }

            [Display(Name = "Remember Me")]
            public bool RememberMe { get; init; } = false;
        }
        public async Task<string> LoginAsync(string username, string password)
        {

          
            // Replace with your API's login endpoint
            var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", new LoginViewModel { EmailOrUserName = username, Password = password });

            if (response.IsSuccessStatusCode)
            {
                // Assuming the API returns a JWT token upon successful login
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                // Handle login failure here (e.g., set error message)
                return null;
            }
        }
    }
}
