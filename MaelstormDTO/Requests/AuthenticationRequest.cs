using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MaelstormDTO.Requests
{
    public class AuthenticationRequest
    {
        [Required(ErrorMessage = "Login is required")]        
        [MinLength(4, ErrorMessage = "Login is too short")]
        [MaxLength(20, ErrorMessage = "Login is too long")]        
        [JsonProperty]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [MinLength(10, ErrorMessage = "Password is too short. Minimum length is 10")]
        [MaxLength(60, ErrorMessage = "Password is too long. Maximum length is 60")]
        [DataType(DataType.Password)]
        [JsonProperty]
        public string Password { get; set; }
        [JsonProperty]
        public string App { get; set; }
        [JsonProperty]
        public string Os { get; set; }
        [JsonProperty]
        public string Fingerprint { get; set; }
    }
}