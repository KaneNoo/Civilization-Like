namespace CivilizationAPI.Services.MissionService
{
    public class MissionService : IMissionService
    {
        private readonly DataContext _context;
        private readonly ILoggingService _loggingService;

        public MissionService(DataContext context, ILoggingService loggingService)
        {
            _context = context;
            _loggingService = loggingService;
        }
        public async Task<ServiceResponse<bool>> Add(Mission mission, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Error", "Mission", "Add", adminName, string.Empty);
            try
            {

                await _context.Missions.AddAsync(mission);
                await _context.SaveChangesAsync();
                response.Data = true;

                log.StackTrace = $"{adminName} добавил новое задание:\n{mission}";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Возникла ошибка при добавлении новой задачи";

                log.Type = "Error";
                log.StackTrace = $"{response.Message}\n{ex.ToString()}";

                
            }
            await _loggingService.LogToDB(log);
            return response;
        }

        public async Task<ServiceResponse<bool>> Update(Mission updatedMission, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Error", "Mission", "Add", adminName, string.Empty);
            try
            {
                var previousMission = await _context.Missions.FirstOrDefaultAsync(m => m.ID == updatedMission.ID);
                if (previousMission != null)
                {
                    string previous = previousMission.ToString();

                    previousMission = updatedMission;
                    _context.Missions.Update(previousMission);
                    await _context.SaveChangesAsync();


                    log.StackTrace = $"{adminName} обновил задание:\n{previous}\n{updatedMission}";
                }
                else
                {
                    throw new Exception("Изменяемое задание отсутствует");
                }


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                log.Type = "Error";
                log.StackTrace = $"{ex}";


            }
            await _loggingService.LogToDB(log);
            return response;
        }

        public async Task<ServiceResponse<bool>> AttachMissionToPlayer(PlayerMission playerMission, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Error", "Mission", "AttachToPlayer", adminName, string.Empty);
            try
            {
                var player = await _context.Players.FirstOrDefaultAsync(p => p.ID == playerMission.PlayerID);
                var mission = await _context.Missions.FirstOrDefaultAsync(m => m.ID == playerMission.MissionID);

                if (player != null && mission != null)
                {

                    playerMission.Mission = mission;
                    playerMission.Player = player;

                    await _context.PlayersMissions.AddAsync(playerMission);
                    await _context.SaveChangesAsync();

                    response.Data = true;

                    log.StackTrace = $"{adminName} добавил новое задание:\n{mission.ToString()}";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Возникла ошибка при закреплении задачи";

                log.Type = "Error";
                log.StackTrace = $"{response.Message}\n{ex.ToString()}";

            }
            await _loggingService.LogToDB(log);

            return response;
        }

        public async Task<ServiceResponse<int>> Complete(PlayerMission playerMission)
        {
            var response = new ServiceResponse<int>();

            try
            {
                playerMission = await _context.PlayersMissions
                    .FirstOrDefaultAsync(
                    pm => pm.MissionID == playerMission.MissionID &&
                    pm.PlayerID == playerMission.PlayerID);

                if (playerMission != null)
                {
                    var district = await _context.Districts.FirstOrDefaultAsync(d => d.Player.ID == playerMission.PlayerID);
                    var mission = await _context.Missions.FirstOrDefaultAsync(m => m.ID == playerMission.MissionID);

                    playerMission.Confirmed = true;

                    _context.PlayersMissions.Update(playerMission);

                    district.CulturePoints += mission.CulturePointsReward;

                    if (district.CulturePoints >= district.Era.CulturePointsRequared)
                    {
                        var newEra = await _context.Eras.FirstOrDefaultAsync(e => e.Level > district.Era.Level);

                        if (newEra != null)
                        {
                            district.CulturePoints -= district.Era.CulturePointsRequared;

                            district.Era = newEra;
                            district.EraID = newEra.ID;

                            _context.Districts.Update(district);
                        }
                    }

                    await _context.SaveChangesAsync();

                    response.Data = mission.CulturePointsReward;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                var log = new Log("Error", "Mission", "CompleteMission", string.Empty, ex.StackTrace);
                await _loggingService.LogToDB(log);
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> Remove(int missionID, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Error", "Mission", "Delete", adminName, string.Empty);
            try
            {
                var mission = await _context.Missions.FirstOrDefaultAsync(m => m.ID == missionID);
                if (mission != null)
                {

                    _context.Missions.Remove(mission);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                log.Type = "Error";
                log.StackTrace = $"{response.Message}\n{ex.ToString()}";
            }
            await _loggingService.LogToDB(log);

            return response;
        }

        public async Task<ServiceResponse<List<Mission>>> GetAll()
        {
            var response = new ServiceResponse<List<Mission>>();

            var missions = await _context.Missions.ToListAsync();

            if (missions.Count > 0)
            {
                response.Data = missions;
            }
            else
            {
                response.Success = false;
                response.Message = "Список заданий пуст";
            }

            return response;
        }

        public async Task<ServiceResponse<Mission>> Get(int missionID)
        {
            var response = new ServiceResponse<Mission>();

            var mission = await _context.Missions.FirstOrDefaultAsync(m => m.ID == missionID);

            if (mission != null)
            {
                response.Data = mission;
            }
            else
            {
                response.Success = false;
                response.Message = "Задание не существует";
            }

            return response;
        }
    }
}
