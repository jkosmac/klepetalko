using klepetalko.Data;
using klepetalko.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class FriendshipController : Controller
{
    private readonly klepet _context;

    public FriendshipController(klepet context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
    var currentUser = await _context.Users
        .Include(u => u.Friendships)
        .ThenInclude(f => f.Friend)
        .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

    return View(currentUser.Friendships);
    }

    [HttpPost]
    public async Task<IActionResult> AddFriend(string friendUsername)
    {
        var currentUser = await _context.Users
            .Include(u => u.Friendships)
            .ThenInclude(f => f.Friend)
            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

        var friend = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == friendUsername);

        if (friend == null || currentUser == null)
        {
            return NotFound("User not found.");
        }

        if (currentUser.Friendships == null || currentUser.Friendships.Any(f => f.Friend.UserName == friendUsername))
        {
            return RedirectToAction("Index");
        }

        var friendship = new Friendship
        {
            Friend = friend,
            FriendshipTime = DateTime.Now
        };

        currentUser.Friendships.Add(friendship);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveUser(string userIdToRemove)
    {
        var currentUser = await _context.Users
            .Include(u => u.Friendships)
                .ThenInclude(f => f.Friend)
            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

        var userToBlock = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userIdToRemove);

        if (userToBlock == null || currentUser == null)
        {
            return NotFound("User not found.");
        }

        var friendship = currentUser.Friendships
            .FirstOrDefault(f => f.Friend.Id == userToBlock.Id);

        if (friendship != null)
        {
            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }


}
