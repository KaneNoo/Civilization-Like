

namespace CivilizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlayerController : ControllerBase
    {

        private readonly IPlayerService _playerService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlayerController(IPlayerService playerService, IHttpContextAccessor httpContextAccessor)
        {
            _playerService = playerService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")] 

        public async Task<ActionResult<ServiceResponse<List<Player>>>> GetAllPlayersAsync()
        {
            var response = await _playerService.GetAll();
            return Ok(response);
        }

        [HttpGet("Get/{username}")]
        public async Task<ActionResult<ServiceResponse<Player>>> GetPlayerAsync(string username)
        {
            var response = await _playerService.Get(username);
            return Ok(response);
        }

        [HttpGet("GetPrizes/{playerID}")]
        public async Task<ActionResult<ServiceResponse<List<Prize>>>> GetPlayerPrizesAsync(int playerID)
        {
            var response = await _playerService.GetPlayerPrizes(playerID);
            return Ok(response);
        }

        [HttpPost("SetAdminRole")] 
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> SetAdminRole([FromBody] string userName)
        {
            var response = await _playerService.SetAdminRole(userName, _httpContextAccessor.HttpContext.User.Identity.Name);
            return Ok(response);
        }

        [HttpDelete("Remove")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemovePlayerAsync([FromBody] int playerID)
        {
            var response = await _playerService.Remove(playerID, _httpContextAccessor.HttpContext.User.Identity.Name);
            return Ok(response);
        }
    }
}
