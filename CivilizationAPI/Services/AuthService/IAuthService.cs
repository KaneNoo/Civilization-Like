namespace CivilizationAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<Player>> Login(string userName);
        Task<ServiceResponse<string>> CreateToken(Player player);
    }
}
