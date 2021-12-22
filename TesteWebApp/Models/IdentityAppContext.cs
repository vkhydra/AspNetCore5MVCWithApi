using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TesteWebApp.Models;

namespace TesteWebApp.Models
{
    public class IdentityAppContext : IdentityDbContext<AppUserModel, AppRoleModel, int>
    {
        public IdentityAppContext(DbContextOptions<IdentityAppContext> options) : base(options) { }
    }
}
