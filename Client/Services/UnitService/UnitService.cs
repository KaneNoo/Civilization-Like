namespace Client.Services.UnitService
{
    public class UnitService : IUnitService
    {
        private readonly HttpClient _client;

        public List<Unit> Units { get; set; } = new List<Unit>();
        public string Message { get; set; }

        public UnitService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ServiceResponse<Unit>> Get(int unitID)
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<Unit>>($"Unit/Get/{unitID}");

            return response;
        }

        public async Task GetAll()
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<List<Unit>>>("Unit/GetAll");

            if (response != null && response.Data != null)
            {
                Units = response.Data;
            }
            else
            {
                Units = new List<Unit>();
            }

            if (Units.Count == 0)
            {
                Message = "Список Форматов пуст";
            }
        }
        public async Task<ServiceResponse<bool>> Add(Unit unit)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Unit/Add", unit);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при добавлении нового Формата";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> Update(Unit unit)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Unit/Update", unit);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при добавлении нового Формата";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            }

            return result;
        }


        public async Task<ServiceResponse<bool>> Remove(int unitID)
        {
            var result = new ServiceResponse<bool>();

            var response = await _client.PostAsJsonAsync("Unit/Remove", unitID);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при удалении Формата";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            }

            return result;
        }
    }
}
