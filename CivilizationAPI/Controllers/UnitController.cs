using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CivilizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitController(IUnitService unitService, IHttpContextAccessor httpContextAccessor)
        {
            _unitService = unitService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Unit>>>> GetAll()
        {
            var response = await _unitService.GetAll();
            return response;
        }

        [HttpGet("Get/{unitID}")]
        public async Task<ActionResult<ServiceResponse<Unit>>> Get(int unitID)
        {
            var response = await _unitService.Get(unitID);
            return response;
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> Add(Unit unit)
        {
            var response = await _unitService.Add(unit, _httpContextAccessor.HttpContext.User.Identity.Name);
            return Ok(response);
        }

        [HttpDelete("Remove")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> Remove(int unitID)
        {
            var response = await _unitService.Remove(unitID, _httpContextAccessor.HttpContext.User.Identity.Name);
            return Ok(response);
        }
    }
}
