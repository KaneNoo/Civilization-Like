
namespace CivilizationAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PrizeController : ControllerBase
    {
        private readonly IPrizeService _prizeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PrizeController(IPrizeService prizeService, IHttpContextAccessor httpContextAccessor)
        {
            _prizeService = prizeService;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Prize>>>> GetAllPrizesAsync()
        {
            var response = await _prizeService.GetAll();
            return Ok(response);
        }

        [HttpGet("Get/{prizeID}")]
        public async Task<ActionResult<ServiceResponse<Prize>>> GetPrizeAsync(int prizeID)
        {
            var response = await _prizeService.Get(prizeID);
            return Ok(response);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddNewPrizeAsync([FromBody] Prize prize)
        {
            var response = await _prizeService.Add(prize, _httpContextAccessor.HttpContext.User.Identity.Name);
            return Ok(response);
        }

        [HttpDelete("Remove")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeletePrizeAsyncAsync([FromBody] int prizeID)
        {
            var response = await _prizeService.Remove(prizeID, _httpContextAccessor.HttpContext.User.Identity.Name);
            return Ok(response);
        }



    }
}
