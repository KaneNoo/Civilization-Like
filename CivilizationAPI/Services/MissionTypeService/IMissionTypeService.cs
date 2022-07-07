namespace CivilizationAPI.Services.MissionTypeService
{
    public interface IMissionTypeService
    {
        List<MissionType> MissionTypes { get; set; }
        string Message { get; set; }
        Task<ServiceResponse<List<MissionType>>> GetAll();
        Task<ServiceResponse<MissionType>> Get(int missionTypeID);
    }
}
