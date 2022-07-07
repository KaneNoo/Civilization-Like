
namespace Client.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly HttpClient _client;

        public PlayerService(HttpClient client)
        {
            _client = client;
        }

        public List<Player> Players { get; set; } = new();
        public string Message { get; set; } = string.Empty;

        public async Task<ServiceResponse<string>> SetAdminRole(string userName)
        {
            var result = new ServiceResponse<string>();
            var response = await _client.PostAsJsonAsync("Player/SetAdminRole", userName);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при удалении пользователя";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            }

            return result;
        }

        public async Task GetAll()
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<List<Player>>>("Player/GetAll");

            if (response != null && response.Data != null)
            {
                Players = response.Data;
            }
            else
            {
                Players = new();
            }

            if (Players.Count == 0)
            {
                Message = "Список Пользователей пуст";
            }
        }

        public async Task<ServiceResponse<Player>> Get(string userName)
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<Player>>($"Player/Get/{userName}");

            return response;
        }

        public async Task<ServiceResponse<bool>> Remove(int playerID)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Player/Remove", playerID);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при удалении пользователя";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            }

            return result;
        }


    }
}
