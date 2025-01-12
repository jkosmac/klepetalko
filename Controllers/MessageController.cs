using klepetalko.Data;
using klepetalko.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class MessageController : Controller
{
    private readonly klepet _context;

    public MessageController(klepet context)
    {
        _context = context;
    }

    public async Task<IActionResult> ChatList()
{
    var currentUser = await _context.Users
        .Include(u => u.Chats)
        .ThenInclude(c => c.Users)
        .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

    if (currentUser == null)
    {
        return Unauthorized();
    }

    var userChats = currentUser.Chats.ToList();
    return View(userChats);
}


    public async Task<IActionResult> Chat(int chatId)
    {
        var chat = await _context.Chats
            .Include(c => c.Messages)
            .ThenInclude(m => m.Sender)
            .FirstOrDefaultAsync(c => c.Id == chatId);

        if (chat == null)
        {
            return NotFound("Chat not found.");
        }

        return View(chat);
    }

    [HttpPost]
    public async Task<IActionResult> StartChat(string friendId)
    {
        var currentUser = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

        if (currentUser == null)
        {
            return NotFound("Current user not found.");
        }

        var friend = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == friendId);

        if (friend == null)
        {
            return NotFound("Friend not found.");
        }

        var existingChat = await _context.Chats
            .Include(c => c.Users)
            .FirstOrDefaultAsync(c => c.Users.Any(u => u.Id == currentUser.Id) && c.Users.Any(u => u.Id == friend.Id));

        if (existingChat == null)
        {
            var chat = new Chat
            {
                Title = $"{currentUser.UserName} & {friend.UserName}",
                CreatedAt = DateTime.Now,
                isGroupchat = false,
                Status = "Active",
                Users = new List<User> { currentUser, friend }
            };

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", new { chatId = chat.Id });
        }

        return RedirectToAction("Chat", new { chatId = existingChat.Id });
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(int chatId, string messageText)
    {
        var currentUser = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

        if (currentUser == null)
        {
            return Unauthorized();
        }

        var chat = await _context.Chats
            .FirstOrDefaultAsync(c => c.Id == chatId);

        if (chat == null)
        {
            return NotFound("Chat not found.");
        }

        var message = new Message
        {
            Text = messageText,
            TimeCreated = DateTime.Now,
            Sender = currentUser,
            Chat = chat
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        return RedirectToAction("Chat", new { chatId });
    }

    [HttpPost]
    public async Task<IActionResult> EditChatTitle(int chatId, string newTitle)
    {
        var chat = await _context.Chats.FirstOrDefaultAsync(c => c.Id == chatId);
        if (chat == null)
        {
            return NotFound("Chat not found.");
        }

        chat.Title = newTitle;
        await _context.SaveChangesAsync();

        TempData["Message"] = $"Naslov klepetalkota spremenjen na \"{newTitle}\"";
        return RedirectToAction("Chat", new { chatId });
    }

}
