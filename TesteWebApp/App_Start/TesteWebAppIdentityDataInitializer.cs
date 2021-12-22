using Microsoft.AspNetCore.Identity;
using System;
using TesteWebApp.Models;

namespace TesteWebApp.App_Start
{
    public class TesteWebAppIdentityDataInitializer
    {
        internal static void SeedData(UserManager<AppUserModel> userManager, RoleManager<AppRoleModel> roleManager)
        {
            SeedRoles(roleManager);
            SeedUser(userManager);
        }

        private static void SeedRoles(RoleManager<AppRoleModel> roleManager)
        {
            if (!roleManager.RoleExistsAsync("TestWebApp.Master").Result)
            {
                var role = new AppRoleModel()
                {
                    Name= "TestWebApp.Master",
                    Description = "Executa todas operações"
                };
                roleManager.CreateAsync(role).Wait();
            }
            if (!roleManager.RoleExistsAsync("TestWebApp.Admin").Result)
            {
                var role = new AppRoleModel()
                {
                    Name = "TestWebApp.Admin",
                    Description = "Executa todas operações dentro do sistema TestWebApp"
                };
                roleManager.CreateAsync(role).Wait();
            }
            if (!roleManager.RoleExistsAsync("TestWebApp.Usuario").Result)
            {
                var role = new AppRoleModel()
                {
                    Name = "TestWebApp.Usuario",
                    Description = "Executa operações de usuário dentro do sistema TestWebApp"
                };
                roleManager.CreateAsync(role).Wait();
            }

        }

        private static void SeedUser(UserManager<AppUserModel> userManager)
        {
            if (userManager.FindByNameAsync("usuariomaster").Result == null)
            {
                var user = new AppUserModel()
                {
                    UserName = "usuariomaster",
                    Email = "usuariomaster@localhost",
                    NomeCompleto = "Usuario Master"
                };
                var result = userManager.CreateAsync(user, "@123Mudar").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "TestWebApp.Master").Wait();
                }
            }
            if (userManager.FindByNameAsync("usuarioadmin").Result == null)
            {
                var user = new AppUserModel()
                {
                    UserName = "usuarioadmin",
                    Email = "usuarioadmin@localhost",
                    NomeCompleto = "Usuario Admin"
                };
                var result = userManager.CreateAsync(user, "@123Mudar").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "TestWebApp.Admin").Wait();
                }
            }
            if (userManager.FindByNameAsync("usuario").Result == null)
            {
                var user = new AppUserModel()
                {
                    UserName = "usuario",
                    Email = "usuario@localhost",
                    NomeCompleto = "Usuario Usuario"
                };
                var result = userManager.CreateAsync(user, "@123Mudar").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "TestWebApp.Usuario").Wait();
                }
            }
        }
    }
}
