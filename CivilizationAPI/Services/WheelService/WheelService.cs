
namespace CivilizationAPI.Services.WheelService
{
    public class WheelService : IWheelService
    {
        private readonly DataContext _context;
        private readonly ILoggingService _loggingService;

        public WheelService(DataContext context, ILoggingService loggingService)
        {
            _context = context;
            _loggingService = loggingService;
        }
        public async Task<ServiceResponse<List<Wheel>>> GetAll()
        {
            var response = new ServiceResponse<List<Wheel>>();

            var wheels = await _context.Wheels
                .Include(w => w.Prize)
                .ToListAsync();

            if (wheels != null && wheels.Count > 0)
            {
                response.Data = wheels;
            }
            else
            {
                response.Success = false;
                response.Message = "Колесо не заполнено";
            }


            return response;
        }

        public async Task<ServiceResponse<List<Prize>>> GetPrizesInWheel(int wheelID)
        {
            var response = new ServiceResponse<List<Prize>>();

            var prizes = await _context.Wheels
                .Where(w => w.ID == wheelID)
                .Include(w => w.Prize)
                .Select(p => p.Prize)
                .ToListAsync();

            if (prizes != null && prizes.Count > 0)
            {
                response.Data = prizes;
            }
            else
            {
                response.Success = false;
                response.Message = "Колесо не заполнено";
            }


            return response;
        }

        public async Task<ServiceResponse<Prize>> GetReward(int wheelID, string userName)
        {
            var response = new ServiceResponse<Prize>();

            var prizes = await GetPrizesInWheel(wheelID);
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Name == userName);

            var log = new Log("Information", "Wheel", "GetReward", userName, string.Empty);
            try
            {
                if (prizes.Success && prizes.Data.Count > 0 && player != null)
                {
                    if (player.WheelCoins <= 0)
                    {
                        throw new Exception("Нет монет для прокрутки Колеса Фортуны");

                    }

                    prizes.Data = prizes.Data.OrderByDescending(p => p.Chance).ToList();
                    int totalWeight = 0;

                    foreach (var prize in prizes.Data)
                    {
                        totalWeight += prize.Chance;
                    }

                    int randomWeight = new Random().Next(1, totalWeight + 1);

                    int proccessedWeight = 0;
                    foreach (var prize in prizes.Data)
                    {
                        proccessedWeight += prize.Chance;

                        if (randomWeight <= proccessedWeight)
                        {
                            var playerPrize = new PlayerPrize(player, prize);

                            await _context.PlayersPrizes.AddAsync(playerPrize);

                            player.WheelCoins -= 1;
                            _context.Players.Update(player);

                            await _context.SaveChangesAsync();

                            response.Data = prize;

                            log.StackTrace = $"{userName} выиграл приз: {prize.ToString()}";
                            break;
                        }
                    }

                }
                else
                {
                    throw new Exception("Ошибка получения приза");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Произошла ошибка при получении приза";

                log.Type = "Error";
                log.StackTrace = $"{response.Message}\n{ex.ToString()}";
            }
            await _loggingService.LogToDB(log);

            return response;
        }

        public async Task<ServiceResponse<bool>> SetPrizesToWheel(List<Wheel> wheel, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Information", "Wheel", "GetReward", adminName, string.Empty);

            try
            {
                var prizes = await _context.Prizes.Where(p => wheel.Select(w => w.PrizeID).ToList().Contains(p.ID)).ToListAsync();

                //var wheel = new List<Wheel>();

                foreach (var prize in prizes)
                {
                    
                }

                //var previousPrizes = await _context.Wheels
                //    .Where(w => w.ID == wheelID)
                //    .ToListAsync();

                //if (previousPrizes.Count > 0)
                //{
                //    _context.Wheels.RemoveRange(previousPrizes);
                //}

                await _context.Wheels.AddRangeAsync(wheel);
                await _context.SaveChangesAsync();

                log.StackTrace = $"{adminName} обновил призы в колесе: ID {wheel[0].ID}";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ошибка при обновлении призов в колесе";

                log.Type = "Error";
                log.StackTrace = $"{response.Message}\n{ex.ToString()}";
            }

            await _loggingService.LogToDB(log);

            return response;
        }
    }
}
