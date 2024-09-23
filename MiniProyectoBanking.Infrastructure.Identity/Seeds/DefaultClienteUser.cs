using Microsoft.AspNetCore.Identity;
using MiniProyectoBanking.Core.Application.Enums;
using MiniProyectoBanking.Infrastructure.Identity.Entities;
namespace MiniProyectoBanking.Infrastructure.Identity.Seeds
{
    public static class DefaultClienteUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {

            ApplicationUser defaultuser = new()
            {
                UserName = "ClienteUser",
                Email = "Cliente@gmail.com",
                Nombre = "Jhon",
                Apellido = "Doe",
                Cedula = "402-2332-2213",
                EmailConfirmed = true,
            };

            if (userManager.Users.All(u=> u.Id != defaultuser.Id)) 
            { 
                var user = await userManager.FindByEmailAsync(defaultuser.Email);
                if(user == null) 
                { 
                    await userManager.CreateAsync(defaultuser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultuser, Roles.Cliente.ToString());
                }
            }
        }
    }
}