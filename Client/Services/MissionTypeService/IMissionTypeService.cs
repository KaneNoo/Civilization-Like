namespace Client.Services.MissionTypeService
{
    public interface IMissionTypeService
    {
        List<MissionType> MissionTypes { get; set; }
        string Message { get; set; }
        Task GetAll();
        Task<ServiceResponse<MissionType>> Get(int missionTypeID);
    }
}
