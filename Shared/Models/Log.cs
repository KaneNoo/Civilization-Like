

namespace Shared.Models
{
    public class Log
    {

        public Log()
        {

        }

        public Log(string type, string service, string method, string userName , string stackTrace)
        {
            Type = type;
            Service = service;
            Method = method;
            UserName = userName;
            StackTrace = stackTrace;
        }

        [JsonIgnore]
        public int ID { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Service { get; set; }
        
        [Required]
        public string Method { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.Now;


        
        [Required]
        public string StackTrace { get; set; }
    }
}
