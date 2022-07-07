namespace Client.Services.MissionTypeService
{
    public class MissionTypeService : IMissionTypeService
    {
        private readonly HttpClient _client;


        public List<MissionType> MissionTypes { get; set; }
        public string Message { get; set; }

        public MissionTypeService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ServiceResponse<MissionType>> Get(int missionTypeID)
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<MissionType>>($"MissionType/Get/{missionTypeID}");

            return response;
        }

        public async Task GetAll()
        {
            var response = await _client.GetFromJsonAsync<ServiceResponse<List<MissionType>>>("MissionType/GetAll");

            if (response != null && response.Data != null)
            {
                MissionTypes = response.Data;
            }
            else
            {
                MissionTypes = new List<MissionType>();
            }

            if (MissionTypes.Count == 0)
            {
                Message = "Список Типов Заданий пуст";
            }
        }
    }
}
