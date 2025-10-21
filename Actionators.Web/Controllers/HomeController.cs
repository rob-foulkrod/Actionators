using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Actionators.Web.Models;
using Actionators.Web.Repositories;

namespace Actionators.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IContactMessageRepository _contactRepository;

    public HomeController(ILogger<HomeController> logger, IContactMessageRepository contactRepository)
    {
        _logger = logger;
        _contactRepository = contactRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactMessage model)
    {
        if (ModelState.IsValid)
        {
            await _contactRepository.AddAsync(model);
            _logger.LogInformation("Contact message received from {Name} ({Email})", model.Name, model.Email);
            TempData["SuccessMessage"] = "Thank you for your message! We'll get back to you soon.";
            return RedirectToAction(nameof(Contact));
        }

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
