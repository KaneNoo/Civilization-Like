namespace CivilizationAPI.Services.EraService
{
    public class EraService : IEraService
    {
        private readonly DataContext _context;
        private readonly ILoggingService _loggingService;

        public EraService (DataContext context, ILoggingService loggingService)
        {
            _context = context;
            _loggingService = loggingService;
        }
        public async Task<ServiceResponse<Era>> Get(int eraID)
        {
            var response = new ServiceResponse<Era>();

            var era = await _context.Eras.FirstOrDefaultAsync(e => e.ID == eraID);

            if (era != null)
            {
                response.Data = era;
            }
            else
            {
                response.Success = false;
                response.Message = "Эра не существует";
            }

            return response;
        }

        public async Task<ServiceResponse<List<Era>>> GetAll()
        {
            var response = new ServiceResponse<List<Era>>();

            var eras = await _context.Eras.ToListAsync();

            if (eras.Count > 0)
            {
                response.Data = eras;
            }
            else
            {
                response.Success = false;
                response.Message = "Эры отсутствуют";
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> Add(Era era, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Information", "Era", "Add", adminName, string.Empty);

            try
            {
                if (_context.Eras.Any(e => e.Name == era.Name))
                {
                    throw new Exception("Эра с таким именем уже существует");
                }

                if (_context.Eras.Any(e => e.Level == era.Level))
                {
                    throw new Exception("Эра с таким уровнем развития уже существует");
                }

                await _context.Eras.AddAsync(era);
                await _context.SaveChangesAsync();

                log.StackTrace = $"{adminName} добавил новую Эру: {era}";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;

                log.Type = "Error";
                log.StackTrace = ex.ToString();
            }

            await _loggingService.LogToDB(log);

            return response;
        }

        public async Task<ServiceResponse<bool>> Update(Era updatedEra, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Information", "Era", "Add", adminName, string.Empty);

            try
            {
                var previousEra = await _context.Eras.FirstOrDefaultAsync(e => e.ID == updatedEra.ID);

                if (previousEra != null)
                {
                    string previous = previousEra.ToString();

                    previousEra = updatedEra;

                    _context.Eras.Update(previousEra);
                    await _context.SaveChangesAsync();

                    log.StackTrace = $"{adminName} изменил Эру:\n{previousEra}\n{updatedEra}";
                }
                else
                {
                    throw new Exception("Изменяемая Эра отсутствует");
                }



                
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;

                log.Type = "Error";
                log.StackTrace = ex.ToString();
            }

            await _loggingService.LogToDB(log);

            return response;
        }



        public async Task<ServiceResponse<bool>> Remove(int eraID, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Information", "Era", "Add", adminName, string.Empty);

            try
            {
                var era = await _context.Eras.FirstOrDefaultAsync(e => e.ID == eraID);

                if(era != null)
                {
                    _context.Eras.Remove(era);
                    await _context.SaveChangesAsync();

                    log.StackTrace = $"{adminName} добавил новую Эру: {era}";
                }
                else
                {
                    throw new Exception("Указанная Эра отсутствует");
                }


            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;

                log.Type = "Error";
                log.StackTrace = ex.ToString();
            }

            await _loggingService.LogToDB(log);

            return response;
        }

    }
}
