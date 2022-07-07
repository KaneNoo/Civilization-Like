using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CivilizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MissionTypeController : ControllerBase
    {
        private readonly IMissionTypeService _missionTypeService;

        public MissionTypeController(IMissionTypeService missionTypeService)
        {
            _missionTypeService = missionTypeService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Mission>>>> GetAllAsync()
        {
            var response = await _missionTypeService.GetAll();
            return Ok(response);
        }

        [HttpGet("Get/{missionTypeID}")]
        public async Task<ActionResult<ServiceResponse<MissionType>>> GetMissionTypeAsync(int missionTypeID)
        {
            var response = await _missionTypeService.Get(missionTypeID);
            return response;
        }
    }
}
