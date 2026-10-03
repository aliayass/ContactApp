using ContactApp.Models;

namespace ContactApp.Services;

public class InMemoryContactRepository : IContactRepository
{
    private readonly List<Contact> _contacts;
    private int _nextId = 1;
    public InMemoryContactRepository()
    {
        _contacts = new List<Contact>();

        // seed data
        var seed = new List<Contact>()
        {
            new Contact {FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Company = "Example Inc." , Notes = "Initial contact", Phone = "123-456-7890", Title = "Software Engineer" },
            new Contact {FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Company = "Example Inc.", Notes = "Initial contact", Phone = "098-765-4321", Title = "Product Manager" },
            new Contact {FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", Company = "Example Inc.", Notes = "Initial contact", Phone = "555-555-5555", Title = "Designer" },
            new Contact {FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", Company = "Example Inc.", Notes = "Initial contact", Phone = "555-123-4567", Title = "Sales Associate" },
            new Contact {FirstName = "Charlie", LastName = "Davis", Email = "charlie.davis@example.com", Company = "Example Inc.", Notes = "Initial contact", Phone = "555-987-6543", Title = "Marketing Specialist" },
            new Contact {FirstName = "David", LastName = "Wilson", Email = "david.wilson@example.com", Company = "Example Inc.", Notes = "Initial contact", Phone = "555-555-5555", Title = "HR Manager" }
        };
        foreach (var contact in seed)
        {
            contact.Id = _nextId++;
            _contacts.Add(contact);
        }

    }

    public Contact Add(Contact contact)
    {
        contact.Id = _nextId++;
        _contacts.Add(contact);
        return contact;
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing is null)
        {
            return false;
        }
        _contacts.Remove(existing);
        return true;
    }

    public IEnumerable<Contact> GetAll() =>
        _contacts
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName);



    public Contact? GetById(int id) =>
        _contacts.FirstOrDefault(c => c.Id.Equals(id));
    

    public bool Update(Contact contact)
    {
        var existing = GetById(contact.Id);
        if (existing is null)
        {
            return false;
        }
        existing.FirstName = contact.FirstName;
        existing.LastName = contact.LastName;
        existing.Email = contact.Email;
        existing.Phone = contact.Phone;
        existing.Company = contact.Company;
        existing.Title = contact.Title;
        existing.Notes = contact.Notes;
        return true;
    }
}
