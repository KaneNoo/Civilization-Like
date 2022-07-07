
namespace CivilizationAPI.Services.PrizeService
{
    public interface IPrizeService
    {
        Task<ServiceResponse<List<Prize>>> GetAll();
        Task<ServiceResponse<Prize>> Get(int prizeID);
        Task<ServiceResponse<bool>> Add(Prize prize, string adminName);
        Task<ServiceResponse<bool>> Update(Prize prize, string adminName);
        Task<ServiceResponse<bool>> Remove(int prizeID, string adminName);
        Task<ServiceResponse<bool>> AttachPrize(PlayerPrize playerPrize);
    }
}
