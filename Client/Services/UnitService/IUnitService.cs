namespace Client.Services.UnitService
{
    public interface IUnitService
    {
        List<Unit> Units { get; set; }
        string Message { get; set; }

        Task<ServiceResponse<Unit>> Get(int id);
        Task GetAll();
        Task<ServiceResponse<bool>> Add(Unit unit);
        Task<ServiceResponse<bool>> Update(Unit unit);
        Task<ServiceResponse<bool>> Remove(int unitID);

    }
}
