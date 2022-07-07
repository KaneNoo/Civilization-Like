namespace CivilizationAPI.Services.EraService
{
    public interface IEraService
    {
        Task<ServiceResponse<Era>> Get(int eraID);
        Task<ServiceResponse<List<Era>>> GetAll();
        Task<ServiceResponse<bool>> Add(Era era, string adminName);
        Task<ServiceResponse<bool>> Update(Era era, string adminName);
        Task<ServiceResponse<bool>> Remove(int eraID, string adminName);


        
    }
}
