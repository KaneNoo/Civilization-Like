global using Shared.Models;
global using Shared.Models.Civilization;
global using Shared.Models.FortuneWheel;


global using System.Text.Json.Serialization;
global using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
