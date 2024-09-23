using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniProyectoBanking.Infrastructure.Identity.Contexts;
using MiniProyectoBanking.Infrastructure.Identity.Entities;
using MiniProyectoBanking.Infrastructure.Identity.Services;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using Microsoft.Extensions.Hosting;
using MiniProyectoBanking.Infrastructure.Identity.Seeds;

namespace MiniProyectoBanking.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            }
            #endregion

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User/AccessDenied";
            });

            services.AddAuthentication();
            #region Services
            services.AddTransient<IAccountService, AccountService>();

            #endregion
            #endregion
        }
        public async static Task AddIdentitySedds(this IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;

                try
                {
                    var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedAsync(roleManager);
                    await DefaultClienteUser.SeedAsync(userManager);
                    await DefaultSuperAdminUser.SeedAsync(userManager);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
