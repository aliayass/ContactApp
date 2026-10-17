using ContactApp.Models;
using ContactApp.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace ContactApp.Repositories
{
    public static class DbSeeder
    {
        public static async Task Seed
            (ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            if (db.Contacts.Any())
            {
                return;
            }

            var seed = new List<Contact>()
        {
            new Contact {FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Company = "Example Inc." , Notes = "Initial contact", Phone = "123-456-7890", Title = "Software Engineer" },
            new Contact {FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Company = "Example Inc.", Notes = "Initial contact", Phone = "098-765-4321", Title = "Product Manager" },
            new Contact {FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", Company = "Example Inc.", Notes = "Initial contact", Phone = "555-555-5555", Title = "Designer" },
            new Contact {FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", Company = "Example Inc.", Notes = "Initial contact", Phone = "555-123-4567", Title = "Sales Associate" },
            new Contact {FirstName = "Charlie", LastName = "Davis", Email = "charlie.davis@example.com", Company = "Example Inc.", Notes = "Initial contact", Phone = "555-987-6543", Title = "Marketing Specialist" },
            new Contact {FirstName = "David", LastName = "Wilson", Email = "david.wilson@example.com", Company = "Example Inc.", Notes = "Initial contact", Phone = "555-555-5555", Title = "HR Manager" }
        };
            db.Contacts.AddRange(seed);
            db.SaveChanges();

            //Seed Role
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
            }

            //Seed Admin User
            if (await userManager.FindByNameAsync("admin") == null)
            {
                var adminUser = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(adminUser, "65admin65");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
