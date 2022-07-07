namespace CivilizationAPI.Services.DistrictService
{
    public class DistrictService : IDistrictService
    {
        private readonly DataContext _context;
        private readonly ILoggingService _loggingService;

        public DistrictService(DataContext context, ILoggingService loggingService)
        {
            _context = context;
            _loggingService = loggingService;
        }

        public async Task<ServiceResponse<District>> Get(int districtID)
        {
            var response = new ServiceResponse<District>();

            var district = await _context.Districts
                .Include(d => d.Unit)
                .FirstOrDefaultAsync(d => d.ID == districtID);

            if (district != null)
            {
                response.Data = district; 
            }
            else
            {
                response.Success = false;
                response.Message = "Территория отсутствует";
            }

            return response;
        }

        public async Task<ServiceResponse<List<District>>> GetAll()
        {
            var response = new ServiceResponse<List<District>>();

            var districts = await _context.Districts
                .Include(d => d.Unit)
                .ToListAsync();

            if (districts.Count > 0)
            {
                response.Data = districts;
            }
            else
            {
                response.Success = false;
                response.Message = "Список Территорий пуст";
            }

            return response;
        }

        public async Task<ServiceResponse<List<District>>> GetRating()
        {
            var response = new ServiceResponse<List<District>>();

            var orderedDistricts = await _context.Districts
                .OrderByDescending(d => d.Era.Level)
                .ThenByDescending(d => d.CulturePoints)
                .ToListAsync();

            if (orderedDistricts.Count > 0)
            {
                response.Data = orderedDistricts;
            }
            else
            {
                response.Success = false;
                response.Message = "Рейтинг пуст";
            }

            return response;
        }
        public async Task<ServiceResponse<bool>> Add(District district, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Information", "District", "Add", adminName, string.Empty);

            try
            {
                if (_context.Districts.Any(u => u.Name == district.Name))
                {
                    throw new Exception("Территория с таким именем уже существует");
                }

                    var unit = await _context.Units.FirstOrDefaultAsync(u => u.ID == district.UnitID);
                    district.Unit = unit;
                    district.UnitID = unit.ID;

                try
                {
                    var era = await _context.Eras.FirstOrDefaultAsync(e => e.Level == 1);
                    district.Era = era;
                    district.EraID = era.ID;
                }
                catch (Exception ex)
                {
                    throw new Exception("Нет Эры уровня 1");
                }

                await _context.Districts.AddAsync(district);
                await _context.SaveChangesAsync();

                log.StackTrace = $"{adminName} добавил новую Территорию: {district}";
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;

                log.Type = "Error";
                log.StackTrace = ex.ToString();
            }
            await _loggingService.LogToDB(log);

            return response;
        }

        public async Task<ServiceResponse<bool>> Update(District updatedDistrict, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Information", "District", "Add", adminName, string.Empty);

            try
            {
                var previousDistrict = await _context.Districts.FirstOrDefaultAsync(d => d.ID == updatedDistrict.ID);
                
                if (previousDistrict != null)
                {
                    string previous = previousDistrict.ToString();
                    previousDistrict = updatedDistrict;
                    _context.Districts.Update(previousDistrict);

                    await _context.SaveChangesAsync();

                    log.StackTrace = $"{adminName} обновил Территорию:\n {previous}\n{updatedDistrict}";
                }
                else
                {
                    throw new Exception("Изменяемая территория отсутствует");
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

        public async Task<ServiceResponse<bool>> Remove(int districtID, string adminName)
        {
            var response = new ServiceResponse<bool>();
            var log = new Log("Information", "District", "Remove", adminName, string.Empty);

            try
            {
                var district = await _context.Districts.FirstOrDefaultAsync(d => d.ID == districtID);
                if (district != null)
                {
                    _context.Districts.Remove(district);
                    await _context.SaveChangesAsync();

                    log.StackTrace = $"{adminName} удалил Территорию: {district.ToString()}";
                }
                else
                {
                    throw new Exception("Территория не существует");
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
