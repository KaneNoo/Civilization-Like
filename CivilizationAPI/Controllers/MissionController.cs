using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CivilizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MissionController : ControllerBase
    {
        private readonly IMissionService _missionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MissionController(IMissionService missionService, IHttpContextAccessor httpContextAccessor)
        {
            _missionService = missionService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Mission>>>> GetAllAsync()
        {
            var response = await _missionService.GetAll();
            return Ok(response);
        }

        [HttpGet("Get/{missionID}")]
        public async Task<ActionResult<ServiceResponse<Mission>>> GetMissionAsync(int missionID)
        {
            var response = await _missionService.Get(missionID);
            return response;
        }


        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddMission([FromBody] Mission mission)
        {
            var response = await _missionService.Add(mission, _httpContextAccessor.HttpContext.User.Identity.Name);
            return response;
        }

        [HttpPost("Complete")]
        public async Task<ActionResult<ServiceResponse<int>>> CompleteMission([FromBody] PlayerMission playerMission)
        {
            var response = await _missionService.Complete(playerMission);
            return Ok(response);
        }

        [HttpPost("AttachMission")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> AttachMissionToPlayer(PlayerMission playerMission)
        {
            var response = await _missionService.AttachMissionToPlayer(playerMission, _httpContextAccessor.HttpContext.User.Identity.Name);
            return response;
        }

        [HttpDelete("Remove")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteMission([FromBody] int missionID)
        {
            var response = await _missionService.Remove(missionID, _httpContextAccessor.HttpContext.User.Identity.Name);
            return response;
        }

    }
}
