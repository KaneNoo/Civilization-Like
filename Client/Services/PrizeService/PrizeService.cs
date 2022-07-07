
namespace Client.Services.PrizeService
{
    public class PrizeService : IPrizeService
    {
        private readonly HttpClient _client;

        public PrizeService(HttpClient client)
        {
            _client = client;
        }

        public List<Prize> Prizes { get; set; } = new();
        public string Message { get; set; } = string.Empty;

        public async Task<ServiceResponse<bool>> Add(Prize prize)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Prize/Add", prize);

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

        public async Task<ServiceResponse<bool>> Update(Prize prize)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Prize/Update", prize);

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

        public async Task<ServiceResponse<bool>> Remove(int prizeID)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Prize/Remove", prizeID);

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

        public async Task GetAll()
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<List<Prize>>>("Prize/GetAll");

            if (response != null && response.Data != null)
            {
                Prizes = response.Data;
            }
            else
            {
                Prizes = new();
            }

            if (Prizes.Count == 0)
            {
                Message = "Список Пользователей пуст";
            }
        }

        public async Task<ServiceResponse<Prize>> Get(int prizeID)
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<Prize>>($"Prize/Get/{prizeID}");

            return response;
        }
    }
}
