using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CivilizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _districtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DistrictController(IDistrictService districtService, IHttpContextAccessor httpContextAccessor)
        {
            _districtService = districtService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<District>>>> GetAll()
        {
            var response = await _districtService.GetAll();
            return Ok(response);
        }

        [HttpGet("Get/{districtID}")]
        public async Task<ActionResult<ServiceResponse<District>>> Get(int districtID)
        {
            var response = await _districtService.Get(districtID);
            return Ok(response);
        }

        [HttpGet("Rating")]
        public async Task<ActionResult<ServiceResponse<List<District>>>> GetRating()
        {
            var response = await _districtService.GetRating();
            return Ok(response);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ServiceResponse<List<District>>>> Add(District district)
        {
            var response = await _districtService.Add(district, _httpContextAccessor.HttpContext.User.Identity.Name);
            return Ok(response);
        }

        [HttpDelete("Remove")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<District>>>> Remove(int districtID)
        {
            var response = await _districtService.Remove(districtID, _httpContextAccessor.HttpContext.User.Identity.Name);
            return Ok(response);
        }
    }
}
