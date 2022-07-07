
namespace CivilizationAPI.Services.WheelService
{
    public interface IWheelService
    {

        Task<ServiceResponse<List<Wheel>>> GetAll();
        Task<ServiceResponse<List<Prize>>> GetPrizesInWheel(int wheelID);
        Task<ServiceResponse<bool>> SetPrizesToWheel(List<Wheel> wheel, string adminName);
        Task<ServiceResponse<Prize>> GetReward(int wheelID, string userName);
    }
}
