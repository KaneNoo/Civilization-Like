namespace Client.Services.EraService
{
    public interface IEraService
    {
        List<Era> Eras { get; set; }
        string Message { get; set; }
        Task<ServiceResponse<Era>> Get(int eraID);
        Task GetAll();
        Task<ServiceResponse<bool>> Add(Era era);
        Task<ServiceResponse<bool>> Update(Era era);
        Task<ServiceResponse<bool>> Remove(int eraID);


        
    }
}
