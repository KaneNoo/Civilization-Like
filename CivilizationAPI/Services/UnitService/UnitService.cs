namespace CivilizationAPI.Services.UnitService
{
    public class UnitService : IUnitService
    {
        private readonly DataContext _context;
        private readonly ILoggingService _loggingService;

        public UnitService(DataContext context, ILoggingService loggingService)
        {
            _context = context;
            _loggingService = loggingService;
        }

        public async Task<ServiceResponse<Unit>> Get(int unitID)
        {
            var response = new ServiceResponse<Unit>();

            var unit = await _context.Units.FirstOrDefaultAsync(u => u.ID == unitID);

            if (unit != null)
            {
                response.Data = unit;
            }
            else
            {
                response.Success = false;
                response.Message = "Формат не существует";
            }

            return response;
        }

        public async Task<ServiceResponse<List<Unit>>> GetAll()
        {
            var response = new ServiceResponse<List<Unit>>();

            var units = await _context.Units.ToListAsync();

            if(units.Count > 0)
            {
                response.Data = units;
            }
            else
            {
                response.Success = false;
                response.Message = "Список форматов пуст";
            }

            return response;
        }
        public async Task<ServiceResponse<bool>> Add(Unit unit, string adminName)
        {
            var log = new Log("Information", "Unit", "Add", adminName, string.Empty);

            var response = new ServiceResponse<bool>();

            try
            {
                if (_context.Units.Any(u => u.Name == unit.Name))
                {
                    throw new Exception("Формат с таким именем уже существует");
                }

                await _context.Units.AddAsync(unit);
                await _context.SaveChangesAsync();

                log.StackTrace = $"{adminName} добавил новый Формат:\n{unit.ToString()}";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                log.Type = "Error";
                log.StackTrace = ex.ToString();
            }
            await _loggingService.LogToDB(log);

            return response;
        }

        public async Task<ServiceResponse<bool>> Remove(int unitID, string adminName)
        {
            var log = new Log("Information", "Unit", "Remove", adminName, string.Empty);

            var response = new ServiceResponse<bool>();

            try
            {
                var unit = await _context.Units.FirstOrDefaultAsync(u => u.ID == unitID);
                if (unit != null)
                {
                    _context.Units.Remove(unit);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Формат не существует");
                }

                log.StackTrace = $"{adminName} добавил новый Формат:\n{unit.ToString()}";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Возникла ошибка при добавлении нового Формата";

                log.Type = "Error";
                log.StackTrace = $"{ex}";
            }
            await _loggingService.LogToDB(log);

            return response;
        }
    }
}
