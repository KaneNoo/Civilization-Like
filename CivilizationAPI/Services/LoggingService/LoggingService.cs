namespace CivilizationAPI.Services.LoggingService
{
    public class LoggingService : ILoggingService
    {
        private readonly DataContext _context;

        public LoggingService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<bool>> LogToDB(Log log)
        {
            
            var response = new ServiceResponse<bool>();

            try
            {
                await _context.Logs.AddAsync(log);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
               
            }

            return response;
        }
    }
}
