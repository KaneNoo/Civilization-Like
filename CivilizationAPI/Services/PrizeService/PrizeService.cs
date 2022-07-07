
namespace CivilizationAPI.Services.PrizeService
{
    public class PrizeService : IPrizeService
    {
        private readonly DataContext _context;
        private readonly ILoggingService _loggingService;

        public PrizeService(DataContext context, ILoggingService loggingService)
        {
            _context = context;
            _loggingService = loggingService;
        }

        public async Task<ServiceResponse<bool>> Add(Prize prize, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Information", "Prize", "Add", adminName, string.Empty);

            try
            {
                await _context.Prizes.AddAsync(prize);
                await _context.SaveChangesAsync();

                log.StackTrace = $"{adminName} добавил новый приз: {prize.ToString()}";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Возникла ошибка при добавлении нового приза";

                log.Type = "Error";
                log.StackTrace = $"{response.Message}\n{ex.ToString()}";

            }
            await _loggingService.LogToDB(log);

            return response;
        }

        public async Task<ServiceResponse<bool>> Update(Prize updatedPrize, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Information", "Prize", "Add", adminName, string.Empty);

            try
            {
                var previousPrize = await _context.Prizes.FirstOrDefaultAsync(p => p.ID == updatedPrize.ID);
                if (previousPrize != null)
                {
                    string previous = previousPrize.ToString();

                    previousPrize = updatedPrize;
                    _context.Prizes.Update(previousPrize);
                    await _context.SaveChangesAsync();

                    log.StackTrace = $"{adminName} обновил приз:\n{previous}\n{updatedPrize}";
                }
                else
                {
                    throw new Exception("Изменяемый приз отсутствует");
                }
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();

                log.Type = "Error";
                log.StackTrace = $"{ex}";

            }
            await _loggingService.LogToDB(log);

            return response;
        }
        public async Task<ServiceResponse<bool>> AttachPrize(PlayerPrize playerPrize)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Information", "Prize", "AttachPrize", string.Empty, string.Empty);
            try
            {
                var player =  await _context.Players.FirstOrDefaultAsync(p => p.ID == playerPrize.PlayerID);
                var prize = await _context.Prizes.FirstOrDefaultAsync(p => p.ID == playerPrize.PrizeID);

                if (player != null && prize != null)
                {
                    playerPrize = new PlayerPrize(player, prize);
                    await _context.PlayersPrizes.AddAsync(playerPrize);
                    await _context.SaveChangesAsync();

                    log.StackTrace = $"{player.Name} выиграл приз: {prize.ToString()}";
                }
                else
                {
                    throw new Exception("Не существует игрок или приз");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Возникла ошибка при закреплении приза";

                log.Type = "Error";
                log.StackTrace = $"{response.Message}\n{ex.ToString()}";

            }
            await _loggingService.LogToDB(log);

            return response;
        }

        public async Task<ServiceResponse<bool>> Remove(int prizeID, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Information", "Prize", "AttachPrize", adminName, string.Empty);

            try
            {
                var prize = await _context.Prizes.FirstOrDefaultAsync(p => p.ID == prizeID);
                if (prize != null)
                {
                    _context.Remove(prize);
                    await _context.SaveChangesAsync();

                    log.StackTrace = $"{adminName} удалил приз: {prize.ToString()}";
                }
                else
                {
                    throw new Exception("Такого приза не существует");
                }
            } 
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Возникла ошибка при удалении приза";

                log.Type = "Error";
                log.StackTrace = $"{response.Message}\n{ex.ToString()}";
            }

            await _loggingService.LogToDB(log);

            return response;
        }

        public async Task<ServiceResponse<List<Prize>>> GetAll()
        {
            var response = new ServiceResponse<List<Prize>>();

            var prizes = await _context.Prizes.ToListAsync();

            if (prizes != null && prizes.Count > 0)
            {
                response.Data = prizes;
            }
            else
            {
                response.Success = false;
                response.Message = "Список призов пуст";
            }

            return response;
        }

        public async Task<ServiceResponse<Prize>> Get(int prizeID)
        {
            var response = new ServiceResponse<Prize>();

            var prize = await _context.Prizes.FirstOrDefaultAsync(p => p.ID == prizeID);

            if (prize != null)
            {
                response.Data = prize;
            }
            else
            {
                response.Success = false;
                response.Message = "Такого приза не существует";
            }


            return response;
        }
    }
}
