using System.ComponentModel.DataAnnotations;

namespace BusinessService.Domain.DomainModel
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
