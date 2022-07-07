using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CivilizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EraController : ControllerBase
    {
        private readonly IEraService _eraService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EraController(IEraService eraService, IHttpContextAccessor httpContextAccessor)
        {
            _eraService = eraService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Era>>>> GetAllAsync()
        {
            var response = await _eraService.GetAll();
            return Ok(response);
        }

        [HttpGet("Get/{eraID}")]
        public async Task<ActionResult<ServiceResponse<Era>>> Get(int eraID)
        {
            var response = await _eraService.Get(eraID);
            return Ok(response);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> Add(Era era)
        {
            var response = await _eraService.Add(era, _httpContextAccessor.HttpContext.User.Identity.Name);
            return Ok(response);
        }

        [HttpDelete("Remove")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> Remove(int eraID)
        {
            var response = await _eraService.Remove(eraID, _httpContextAccessor.HttpContext.User.Identity.Name);
            return Ok(response);
        }


    }
}
