using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TesteWebApp.Models
{
    public class AppRoleModel : IdentityRole<int>
    {
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
