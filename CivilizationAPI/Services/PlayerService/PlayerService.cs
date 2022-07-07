
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;

namespace CivilizationAPI.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {

        private readonly DataContext _context;
        private readonly ILoggingService _loggingService;

        public PlayerService(DataContext context, ILoggingService loggingService)
        {
            _context = context;
            _loggingService = loggingService;
        }

        

        public async Task<ServiceResponse<string>> Add(Player player)
        {
            var response = new ServiceResponse<string>();
            var log = new Log("Information", "Player", "Add", player.Name, string.Empty);

            try
            {
                await _context.AddAsync(player);
                await _context.SaveChangesAsync();

                log.StackTrace = $"Добавлен новый пользователь {player.Name}";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Возникла ошибка при добавлении пользователя";

                log.Type = "Error";
                log.StackTrace = $"{response.Message}\n{ex.ToString()}";
               
            }
            await _loggingService.LogToDB(log);
            return response;
        }

        public async Task<ServiceResponse<string>> SetAdminRole(string userName, string adminName)
        {
            var response = new ServiceResponse<string>();
            var log = new Log("Information", "Player", "SetAdminRole", adminName, string.Empty);
            try
            {
                var player = await _context.Players
                    .FirstOrDefaultAsync(p => p.Name == userName); 

                if (player != null)
                {
                    player.IsAdmin = true;
                    _context.Players.Update(player);
                    await _context.SaveChangesAsync();

                    log.StackTrace = $"{adminName} сделал админом пользователя {userName}";
                }
                else
                {
                    throw new Exception("Изменяемый пользователь отсутствует");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ошибка при присвоении роли Администратора";

                log.Type = "Error";
                log.StackTrace = $"{response.Message}\n{ex.ToString()}";

            }
            await _loggingService.LogToDB(log);

            return response;
        }

        public async Task<ServiceResponse<List<Player>>> GetAll()
        {
            var response = new ServiceResponse<List<Player>>();

            var players = await _context.Players
                .Include(p => p.District)
                .ToListAsync();

            if (players != null && players.Count > 0)
            {
                response.Data = players;
            }
            else
            {
                response.Success = false;
                response.Message = "База игроков пуста";
            }

            return response;
        }

        public async Task<ServiceResponse<Player>> Get(string userName)
        {
            var response = new ServiceResponse<Player>();

            var player = await _context.Players.FirstOrDefaultAsync(x => x.Name == userName);

            if (player != null)
            {
                response.Data = player;
            }
            else
            {
                response.Success = false;
                response.Message = "Такого игрока не существует";
            }

            return response;
        }

        public async Task<ServiceResponse<List<Prize>>> GetPlayerPrizes(int playerID)
        {
            var response = new ServiceResponse<List<Prize>>();
            var playerPrizes = await _context.PlayersPrizes
                .Where(p => p.PlayerID == playerID)
                .Include(p => p.Prize)
                .ToListAsync();

            if (playerPrizes != null && playerPrizes.Count > 0)
            {
                response.Data = playerPrizes
                    .Select(p => p.Prize)
                    .ToList();
            }
            else
            {
                response.Success = false;
                response.Message = "Список наград пуст";
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> Remove(int playerID, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Information", "Player", "Add", adminName, string.Empty);

            try
            {
                var player = await _context.Players.FirstOrDefaultAsync(x => x.ID == playerID);
                if (player != null)
                {
                    _context.Remove(player);
                    await _context.SaveChangesAsync();
                    response.Data = true;

                    log.StackTrace = $"{adminName} удалил пользователя {player.Name}";
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Возникла ошибка при удалении пользователя";

                log.Type = "Error";
                log.StackTrace = $"{response.Message}\n{ex.ToString()}";
            }
            await _loggingService.LogToDB(log);
            return response;
        }

        
    }
}
