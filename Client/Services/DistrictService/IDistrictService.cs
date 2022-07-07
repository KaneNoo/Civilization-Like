namespace Client.Services.DistrictService
{
    public interface IDistrictService
    {
        List<District> Districts{ get; set; }
        string Message { get; set; }

        public Task GetAll();
        public Task GetRating();
        public Task<ServiceResponse<District>> Get(int districtID);

        public Task<ServiceResponse<bool>> Add(District district);
        public Task<ServiceResponse<bool>> Update(District district);

        public Task<ServiceResponse<bool>> Remove(int districtID);

        
    }
}
