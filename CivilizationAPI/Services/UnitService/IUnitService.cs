namespace CivilizationAPI.Services.UnitService
{
    public interface IUnitService
    {
        
        Task<ServiceResponse<Unit>> Get(int id);
        Task<ServiceResponse<List<Unit>>> GetAll();
        Task<ServiceResponse<bool>> Add(Unit unit, string adminName);
        Task<ServiceResponse<bool>> Remove(int unitID, string adminName);

    }
}
