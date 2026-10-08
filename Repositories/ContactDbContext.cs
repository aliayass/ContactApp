using ContactApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactApp.Repositories
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; }          
    }
}
