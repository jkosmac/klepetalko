using klepetalko.Data;
using klepetalko.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class SettingsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly klepet _context;

    public SettingsController(UserManager<User> userManager, klepet context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> ToggleDarkMode()
    {
        var currentUser = await _context.Users
            .Include(u => u.Setting)
            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

        if (currentUser == null)
            return Unauthorized();

        var userSettings = currentUser.Setting;

        if (userSettings == null)
        {
            userSettings = new Setting
            {
                UserId = currentUser.Id,
                darkMode = true
            };
            _context.Settings.Add(userSettings);
        }
        else
        {
            userSettings.darkMode = !userSettings.darkMode;
        }

        await _context.SaveChangesAsync();
        return Json(new { darkMode = userSettings.darkMode });
    }

    [HttpGet]
    public async Task<IActionResult> GetDarkMode()
    {
        var currentUser = await _context.Users
            .Include(u => u.Setting)
            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

        if (currentUser == null)
            return Unauthorized();

        var userSettings = currentUser.Setting;

        return Json(new { darkMode = userSettings?.darkMode ?? false });
    }
}
