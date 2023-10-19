using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Training102.BAL.SharedModel;
using Training102.SharedModel.ViewModel;
using static System.Net.WebRequestMethods;

namespace Training102.SharedUI
{
    public class AuthenticationService 
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

     
    }

    //protected override async Task OnAfterRenderAsync(bool firstRender)
    //{
    //    if (firstRender)
    //    {
    //        var token = await TokenProvider.GetTokenAsync();
    //        Branches = await Http.GetJsonAsync<List<BranchDto>>(
    //            "vip/api/lookup/getbranches",
    //            new AuthenticationHeaderValue("Bearer", token));

    //        StateHasChanged();
    //    }
    //}
    public class CustimStateProvider : AuthenticationStateProvider
    {
  
        private readonly ILocalStorageService _localStorageService;

        public CustimStateProvider( ILocalStorageService localStorageService)
        {
         
            this._localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {


                var getToken =  await _localStorageService.GetItemAsStringAsync("JwtAccessTokken");

                if (string.IsNullOrEmpty(getToken))
                {
                    var auth = new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new ClaimsIdentity()));

                    return auth;
                }
                else
                {
                    var auth = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(getToken), "JwtAuth")));

                    return auth;

                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public void NotifyAuthState()
        {

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
