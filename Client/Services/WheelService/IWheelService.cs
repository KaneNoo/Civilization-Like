
namespace Client.Services.WheelService
{
    public interface IWheelService
    {
        List<Wheel> Wheels { get; set; }
        string Message { get; set; }

        Task GetAll();
        Task<ServiceResponse<List<Wheel>>> GetPrizes(int wheelID);
        Task<ServiceResponse<bool>> Update(List<Wheel> wheel);
        Task<ServiceResponse<Prize>> GetReward(int wheelID);
        Task<ServiceResponse<bool>> Add(List<int> prizesID);
        Task<ServiceResponse<bool>> Remove(int wheelID);
    }
}
