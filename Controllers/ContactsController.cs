using ContactApp.Models;
using ContactApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Controllers;

public class ContactsController : Controller
{
    private readonly IContactRepository _repo;
    private readonly ILogger<ContactsController> _logger;

    public ContactsController(IContactRepository repo, ILogger<ContactsController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index(string q)
    {
        var items = _repo.GetAll();
        if (!string.IsNullOrEmpty(q))
        {
            var term = q.Trim();
            items = items.Where(c =>
            (c.FirstName + " " + c.LastName).Contains(term, StringComparison.CurrentCultureIgnoreCase)
            || c.FirstName.Contains(term, StringComparison.CurrentCultureIgnoreCase)
            || c.LastName.Contains(term, StringComparison.CurrentCultureIgnoreCase));
        }
        ViewData["Title"] = "Kişiler";
        ViewBag.Query = q;
        return View(items.ToList());
    }
    public IActionResult Details(int id)
    {
        var items = _repo.GetById(id);
        if (items is null)
        {
            return NotFoundView();
            ViewData["Title"] = "Kişi Güncelle";
        }
        return View(items);
    }

    private IActionResult NotFoundView()
    {
        Response.StatusCode = 404;
        ViewData["Title"] = "Bulunamadı";
        ViewBag.Message = "Aradığınız kişi bulunamadı.";
        return View("NotFound");
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = "Kişi Ekle";
        return View(new Contact());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Contact contact)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Kişi Ekle";
            return View(contact);
        }
        _repo.Add(contact);
        _logger.LogInformation("Yeni kişi eklendi: {FirstName} {LastName}", contact.FirstName, contact.LastName);
        TempData["Success"] = "Kişi başarıyla eklendi.";
        return RedirectToAction(nameof(Index));
    }


    [HttpGet("Contacts/Edit/{id}")]
    public IActionResult Edit(int id)
    {
        var contact = _repo.GetById(id);
        if (contact is null)
            return NotFoundView();
        ViewData["Title"] = "Kişi Görüntüleme";
        return View(contact);
    }
    [HttpPost("Contacts/Edit/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Contact contact)
    {
        if (id != contact.Id)
            ModelState.AddModelError(string.Empty, "Kişi ID'si eşleşmiyor.");
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Kişi Güncelle";
            return View(contact);
        }
        var ok = _repo.Update(contact);
        if (!ok)
            return NotFoundView();
        _logger.LogInformation("Kişi güncellendi: {FirstName} {LastName}", contact.FirstName, contact.LastName);
        TempData["Success"] = "Kişi başarıyla güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Contacts/Delete/{id}")]
    public IActionResult Delete(int id)
    {
        var contact = _repo.GetById(id);
        if (contact is null)
            return NotFoundView();
        ViewData["Title"] = "Silme Onayı";
        return View(contact);
    }
    [HttpPost("Contacts/Delete/{id}")]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id, Contact contact)
    {
        var ok = _repo.Delete(id);
        if (!ok)
            return NotFoundView();
        _logger.LogInformation("Kişi silindi: {FirstName} {LastName}", contact.FirstName, contact.LastName);
        TempData["Success"] = "Kişi başarıyla silindi.";
        return RedirectToAction(nameof(Index));
    }

}
