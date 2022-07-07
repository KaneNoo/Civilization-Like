namespace CivilizationAPI.Services.DistrictService
{
    public interface IDistrictService
    {
        public Task<ServiceResponse<List<District>>> GetAll();
        public Task<ServiceResponse<District>> Get(int districtID);
        public Task<ServiceResponse<List<District>>> GetRating();
        public Task<ServiceResponse<bool>> Add(District district, string adminName);
        public Task<ServiceResponse<bool>> Update(District district, string adminName);

        public Task<ServiceResponse<bool>> Remove(int districtID, string adminName);

        
    }
}
