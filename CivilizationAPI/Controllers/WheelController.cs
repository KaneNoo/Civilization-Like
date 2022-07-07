

namespace CivilizationAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class WheelController : ControllerBase
    {
        private readonly IWheelService _wheelService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WheelController(IWheelService wheelService, IHttpContextAccessor httpContextAccessor)
        {
            _wheelService = wheelService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Prize>>>> GetAll()
        {
            var response = await _wheelService.GetAll();
            return Ok(response);
        }

        [HttpGet("GetPrizes/{wheelID}")]
        public async Task<ActionResult<ServiceResponse<List<Prize>>>> GetPrizesInWheel(int wheelID)
        {
            var response = await _wheelService.GetPrizesInWheel(wheelID);
            return Ok(response);
        }

        [HttpPost("GetReward")]
        public async Task<ServiceResponse<Prize>> GetReward([FromBody] int wheelID)
        {
            var response = await _wheelService.GetReward(wheelID, _httpContextAccessor.HttpContext.User.Identity.Name);
            return response;
        }

        [HttpPost("SetPrizes")]
        [Authorize(Roles = "Admin")]
        public async Task<ServiceResponse<bool>> setPrizesToWheel([FromBody] List<Wheel> wheel)
        {
            var response = await _wheelService.SetPrizesToWheel(wheel, _httpContextAccessor.HttpContext.User.Identity.Name);
            return response;
        }


    }
}
