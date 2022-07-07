
namespace Client.Services.PrizeService
{
    public interface IPrizeService
    {
        List<Prize> Prizes { get;set; }
        string Message { get; set; }
        Task GetAll();
        Task<ServiceResponse<Prize>> Get(int prizeID);
        Task<ServiceResponse<bool>> Add(Prize prize);
        Task<ServiceResponse<bool>> Update(Prize prize);
        Task<ServiceResponse<bool>> Remove(int prizeID);
    }
}
