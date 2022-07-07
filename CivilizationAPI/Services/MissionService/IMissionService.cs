
namespace CivilizationAPI.Services.MissionService
{
    public interface IMissionService
    {

        Task<ServiceResponse<List<Mission>>> GetAll();
        Task<ServiceResponse<Mission>> Get(int missionID);
        Task<ServiceResponse<bool>> Add(Mission mission, string adminName);
        Task<ServiceResponse<bool>> Update(Mission mission, string adminName);
        Task<ServiceResponse<bool>> AttachMissionToPlayer(PlayerMission playerMission, string adminName);
        Task<ServiceResponse<int>> Complete(PlayerMission playerMission);
        Task<ServiceResponse<bool>> Remove(int missionID, string adminName);

    }

}
