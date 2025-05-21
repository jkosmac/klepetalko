using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using klepetalko.Models;
using klepetalko.Data;
using Microsoft.EntityFrameworkCore;

namespace klepetalko.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public async Task<IActionResult> HasUnreadMessages([FromServices] klepet context)
    {
        var currentUser = await context.Users
            .Include(u => u.Chats)
            .Include(u => u.ReadMessages)
            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

        if (currentUser == null) return Json(false);

        var unread = await context.Messages
            .Where(m => m.Chat.Users.Any(u => u.Id == currentUser.Id)
                        && !m.ReadBy.Any(u => u.Id == currentUser.Id)
                        && m.Sender.Id != currentUser.Id)
            .AnyAsync();

        return Json(unread);
    }

}
