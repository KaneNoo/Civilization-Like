
namespace CivilizationAPI.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<ServiceResponse<List<Player>>> GetAll();
        Task<ServiceResponse<string>> Add(Player player);
        Task<ServiceResponse<bool>> Remove(int playerID, string adminName);
        Task<ServiceResponse<Player>> Get(string userName);
        Task<ServiceResponse<List<Prize>>> GetPlayerPrizes(int playerID);
        Task<ServiceResponse<string>> SetAdminRole(string userName, string adminName);
    }
}
