
namespace Client.Services.MissionService
{
    public interface IMissionService
    {
        List<Mission> Missions { get; set; }
        string Message { get; set; }

        Task GetAll();
        Task<ServiceResponse<Mission>> Get(int missionID);
        Task<ServiceResponse<bool>> Add(Mission mission);
        Task<ServiceResponse<bool>> Update(Mission mission);
        Task<ServiceResponse<bool>> AttachMissionToPlayer(PlayerMission playerMission);
        Task<ServiceResponse<int>> Complete(PlayerMission playerMission);
        Task<ServiceResponse<bool>> Remove(int missionID);

    }

}
