using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using Training102.BAL.SharedModel;
using Training102.SharedModel.ViewModel;

namespace Training102.SharedUI
{
    public class AuthenticationService :AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorageService;

        public AuthenticationService(HttpClient httpClient, IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _httpClientFactory = httpClientFactory;
            this._localStorageService = localStorageService;
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
        public async Task<UserManagerResponse> LoginAsync(string username, string password)
        {

          
            // Replace with your API's login endpoint
            var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", new LoginViewModel { EmailOrUserName = username, Password = password });
            //var httpClient = _httpClientFactory.CreateClient("BasedApi");

            //var response2 = await httpClient.PostAsJsonAsync("/api/Auth/login", new LoginViewModel { EmailOrUserName = username, Password = password });

            if (response.IsSuccessStatusCode)
            {
                // Assuming the API returns a JWT token upon successful login
                var result = await response.Content.ReadFromJsonAsync<UserManagerResponse>();
                
                await _localStorageService.SetItemAsync<string>("JwtAccessTokken", result.Message?.ToString());
                return result;
            }
            else
            {
                // Handle login failure here (e.g., set error message)
                return null;
            }
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var getToken = await  _localStorageService.GetItemAsync<string>("JwtAccessTokken");

            if (string.IsNullOrEmpty(getToken))
            {
                return new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new ClaimsIdentity()));
            }
            else
            {
                

            }
        }
    }
}
