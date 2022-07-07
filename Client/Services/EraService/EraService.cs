namespace Client.Services.EraService
{
    public class EraService : IEraService
    {
        private readonly HttpClient _client;

        public List<Era> Eras { get; set; }
        public string Message { get; set; }

        public EraService (HttpClient client)
        {
            _client = client;
        }
        public async Task<ServiceResponse<Era>> Get(int eraID)
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<Era>>($"Era/Get/{eraID}");

            return response;
        }

        public async Task GetAll()
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<List<Era>>>("Era/GetAll");

            if (response != null && response.Data != null)
            {
                Eras = response.Data;
            }
            else
            {
                Eras = new List<Era>();
            }

            if (Eras.Count == 0)
            {
                Message = "Список Форматов пуст";
            }
        }

        public async Task<ServiceResponse<bool>> Add(Era era)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Era/Add", era);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при добавлении новой Эры";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> Update(Era era)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Era/Update", era);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при добавлении новой Эры";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> Remove(int eraID)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Era/Remove", eraID);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при удалении Эры";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            }

            return result;
        }

    }
}
