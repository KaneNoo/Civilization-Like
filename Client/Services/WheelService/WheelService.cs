
namespace Client.Services.WheelService
{
    public class WheelService : IWheelService
    {
        private readonly HttpClient _client;

        public List<Wheel> Wheels { get; set; }
        public string Message { get; set; }

        public WheelService(HttpClient client)
        {
            _client = client;
        }

        public async Task GetAll()
        {

            var response = await _client.GetFromJsonAsync<ServiceResponse<List<Wheel>>>("Wheel/GetAll");

            if (response != null && response.Data != null)
            {
                Wheels = response.Data;
            }
            else
            {
                Wheels = new();
            }

            if (Wheels.Count == 0)
            {
                Message = "Список Колес Фортуны пуст";
            }
        }

        public async Task<ServiceResponse<List<Wheel>>> GetPrizes(int wheelID)
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<List<Wheel>>>($"Wheel/Get/{wheelID}");

            return response;
        }

        public async Task<ServiceResponse<Prize>> GetReward(int wheelID)
        {
            var result = new ServiceResponse<Prize>();
            var response = await _client.PostAsJsonAsync("Wheel/GetReward", wheelID);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при получении приза";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<Prize>>();
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> Add(List<int> prizesID)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Wheel/Add", prizesID);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при добавлении нового Колеса";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            }

            return result;
        }
        public async Task<ServiceResponse<bool>> Update(List<Wheel> wheel)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Wheel/Update", wheel);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при добавлении нового Колеса";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> Remove(int wheelID)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Wheel/Remove", wheelID);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при добавлении нового Колеса";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            }

            return result;
        }

        
    }
}
