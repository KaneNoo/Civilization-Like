namespace CivilizationAPI.Services.MissionTypeService
{
    public class MissionTypeService : IMissionTypeService
    {
        private readonly DataContext _context;

        public List<MissionType> MissionTypes { get; set; }
        public string Message { get; set; }

        public MissionTypeService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<MissionType>>> GetAll()
        {
            var response = new ServiceResponse<List<MissionType>>();

            var missionTypes = await _context.MissionTypes.ToListAsync();

            if (missionTypes.Count > 0)
            {
                response.Data = missionTypes;
            }
            else
            {
                response.Success = false;
                response.Message = "Список заданий пуст";
            }

            return response;
        }

        public async Task<ServiceResponse<MissionType>> Get(int missionTypeID)
        {
            var response = new ServiceResponse<MissionType>();

            var mission = await _context.MissionTypes.FirstOrDefaultAsync(mt => mt.ID == missionTypeID);

            if (mission != null)
            {
                response.Data = mission;
            }
            else
            {
                response.Success = false;
                response.Message = "Задание не существует";
            }

            return response;
        }
    }
}
