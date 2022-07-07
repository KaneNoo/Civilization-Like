namespace Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<Player>> Authorize();
    }
}
