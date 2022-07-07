using System.DirectoryServices.AccountManagement;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CivilizationAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IPlayerService _playerService;
        private readonly IConfiguration _configuration;

        public AuthService(
            IConfiguration configuration,
            IPlayerService playerService)
        {

            _configuration = configuration;
            _playerService = playerService;
        }

        public async Task<ServiceResponse<Player>> Login(string userName)
        {

            var response = new ServiceResponse<Player>();

            var player = (await _playerService.Get(userName)).Data;
            try
            {
                if (player == null)
                {
                    player = new Player
                    {
                        Name = userName
                    };

                    var newPlayerResponse = await _playerService.Add(player);

                    if (newPlayerResponse != null && newPlayerResponse.Success)
                    {
                        response.Data = player;
                    }
                    else
                    {
                        throw new Exception("Ошибка создания пользователя");
                    }
                }
                var createTokenResponse = await CreateToken(player);
                if (createTokenResponse != null && createTokenResponse.Success)
                {

                    response.Data = player;
                    response.Message = createTokenResponse.Data;
                }
                else
                {
                    throw new Exception("Ошибка присвоения токена");
                }


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public Task<ServiceResponse<string>> CreateToken(Player player)
        {
            var response = new ServiceResponse<string>();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, player.Name),
                new Claim(ClaimTypes.Role, player.IsAdmin ? "Admin" : "Player")
            };

            var apiToken = _configuration.GetSection("WheelAPIToken").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiToken));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            response.Data = jwt;

            return Task.FromResult(response);
        }

    }
}
