using ContactApp.Models;
using ContactApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ContactApp.Repositories;

public class EfContactRepository : IContactRepository
{
    private readonly ContactDbContext _db;

    public EfContactRepository(ContactDbContext db)
    {
        _db = db;
    }

    public Contact Add(Contact contact)
    {
        _db.Contacts.Add(contact);
        _db.SaveChanges();
        return contact;
    }

    public bool Delete(int id)
    {
        var existing = _db.Contacts.Find(id);
        if (existing is null)
        {
            return false;
        }
        _db.Contacts.Remove(existing);
        _db.SaveChanges();
        return true;
    }
    

    public IEnumerable<Contact> GetAll()
    {
        return _db.Contacts.AsNoTracking().OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToList();
    }

    public Contact? GetById(int id)
    {
        return _db.Contacts.AsNoTracking().FirstOrDefault(c => c.Id == id);
    }

    public bool Update(Contact contact)
    {
        var existing = _db.Contacts.FirstOrDefault(c => c.Id == contact.Id);
        if (existing is null)
        {
            return false;
        }
        _db.Entry(existing).CurrentValues.SetValues(contact);
        _db.SaveChanges();
        return true;
    }
}
