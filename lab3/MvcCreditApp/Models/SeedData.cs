using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcCreditApp.Data;

namespace MvcCreditApp.Models
{
    public class SeedData
    {
        public static async Task InitializeRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { "admin", "user" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Create admin user
            var adminEmail = "admin@credit.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                await userManager.CreateAsync(admin, "Admin123!");
                await userManager.AddToRoleAsync(admin, "admin");
            }
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CreditContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CreditContext>>()))
            {
                if (context == null || context.Credits == null)
                {
                    throw new ArgumentNullException("Null CreditContext");
                }

                context.Database.EnsureCreated();

                if (context.Credits.Any())
                {
                    return;
                }

                context.Credits.Add(new Credit { Head = "Ипотечный", Period = 10, Sum = 1000000, Procent = 15 });
                context.Credits.Add(new Credit { Head = "Образовательный", Period = 7, Sum = 300000, Procent = 10 });
                context.Credits.Add(new Credit { Head = "Потребительский", Period = 5, Sum = 500000, Procent = 19 });

                context.Credits.AddRange(
                    new Credit { Head = "Льготный", Period = 12, Sum = 55555, Procent = 7 },
                    new Credit { Head = "Срочный", Period = 3, Sum = 3333, Procent = 19 }
                );

                context.SaveChanges();
            }
        }
    }
}
