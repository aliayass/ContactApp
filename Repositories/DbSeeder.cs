using ContactApp.Models;

namespace ContactApp.Repositories
{
    public static class DbSeeder
    {
        public static void Seed(ContactDbContext db)
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
        }
    }
}
