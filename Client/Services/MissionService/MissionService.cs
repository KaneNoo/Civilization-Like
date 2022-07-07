namespace Client.Services.MissionService
{
    public class MissionService : IMissionService
    {
        private readonly HttpClient _client;

        public MissionService(HttpClient client)
        {
            _client = client;
        }

        public List<Mission> Missions { get; set; }
        public string Message { get; set; }

        public async Task<ServiceResponse<bool>> Add(Mission mission)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Mission/Add", mission);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при добавлении нового задания";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            }

            return result;
        }
        public async Task<ServiceResponse<bool>> Update(Mission mission)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Mission/Update", mission);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при обновлении задания";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> AttachMissionToPlayer(PlayerMission playerMission)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Mission/AttachMissionToPlayer", playerMission);

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

        public async Task<ServiceResponse<int>> Complete(PlayerMission playerMission)
        {
            var result = new ServiceResponse<int>();
            var response = await _client.PostAsJsonAsync("Mission/Complete", playerMission);

            if (response == null || !response.IsSuccessStatusCode)
            {
                result.Success = false;
                result.Message = "Возникла ошибка при добавлении нового Формата";
            }
            else
            {
                result = await response.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> Remove(int missionID)
        {
            var result = new ServiceResponse<bool>();
            var response = await _client.PostAsJsonAsync("Mission/Remove", missionID);

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

        public async Task GetAll()
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<List<Mission>>>("Mission/GetAll");

            if (response != null && response.Data != null)
            {
                Missions = response.Data;
            }
            else
            {
                Missions = new List<Mission>();
            }

            if (Missions.Count == 0)
            {
                Message = "Список Заданий пуст";
            }
        }

        public async Task<ServiceResponse<Mission>> Get(int missionID)
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<Mission>>($"Mission/Get/{missionID}");

            return response;
        }
    }
}
