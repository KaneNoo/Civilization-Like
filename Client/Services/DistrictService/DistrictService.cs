namespace Client.Services.DistrictService
{
    public class DistrictService : IDistrictService
    {
        private readonly HttpClient _client;

        public List<District> Districts { get; set; } = new List<District>();
        public string Message { get; set; } = String.Empty;

        public DistrictService(HttpClient client)
        {
            _client = client;
        }



        public async Task<ServiceResponse<District>> Get(int districtID)
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<District>>($"District/Get/{districtID}");

            return response;
        }

        public async Task GetAll()
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<List<District>>>($"District/GetAll");

            if (response != null && response.Data != null)
            {
                Districts = response.Data;
            }

            if (Districts.Count <= 0)
            {
                Message = "Список Территорий пуст";
            }
        }

        public async Task GetRating()
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<List<District>>>($"District/Rating");

            if (response != null && response.Data != null)
            {
                Districts = response.Data;
            }

            if (Districts.Count <= 0)
            {
                Message = "Список Территорий пуст";
            }
        }
        public async Task<ServiceResponse<bool>> Add(District district)
        {
            var result = new ServiceResponse<bool>();

            var response = await _client.PostAsJsonAsync($"District/Add", district);

            if (response != null && response.IsSuccessStatusCode)
            {
                result.Data = true;
            }
            else
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> Update(District district)
        {
            var result = new ServiceResponse<bool>();

            var response = await _client.PostAsJsonAsync($"District/Update", district);

            if (response != null && response.IsSuccessStatusCode)
            {
                result.Data = true;
            }
            else
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> Remove(int districtID)
        {

            var result = new ServiceResponse<bool>();

            var response = await _client.PostAsJsonAsync($"District/Remove", districtID);

            if (response != null && response.IsSuccessStatusCode)
            {
                result.Data = true;
            }
            else
            {
                result.Success = false;
            }

            return result;
        }
    }
}
