
namespace Client.Services.PlayerService
{
    public interface IPlayerService
    {
        List<Player> Players { get; set; }
        string Message { get; set; }
        Task GetAll();
        Task<ServiceResponse<Player>> Get(string userName);
        Task<ServiceResponse<bool>> Remove(int playerID);

        //Task<ServiceResponse<List<Prize>>> GetPlayerPrizes(int playerID);
        Task<ServiceResponse<string>> SetAdminRole(string userName);
    }
}
