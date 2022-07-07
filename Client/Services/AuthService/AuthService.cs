using System.Security.Claims;
using System.Text;

namespace Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<Player>> Authorize()
        {
            //TODO: Активируется дважды выходе на MainMenu
            var response = await _client.GetFromJsonAsync<ServiceResponse<Player>>("Auth");
            if (response != null && response.Success)
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{response.Message}");
            }
            return response;
        }
    }
}
