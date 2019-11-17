using System.ComponentModel.DataAnnotations;

namespace myJWTAPI.Controllers.Resources
{
    public class RevokeTokenResource
    {
        [Required]
        public string Token { get; set; }
    }
}