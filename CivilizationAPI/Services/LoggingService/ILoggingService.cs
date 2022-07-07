namespace CivilizationAPI.Services.LoggingService
{
    public interface ILoggingService
    {
        Task<ServiceResponse<bool>> LogToDB(Log log); 
    }
}
