using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TesteWebApp.Models
{
    public class AppUserModel : IdentityUser<int>
    {
        [Required]
        [MaxLength(255)]
        public string NomeCompleto { get; set; }
    }
}
