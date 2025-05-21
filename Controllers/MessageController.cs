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
            .Include(u => u.Chats)
                .ThenInclude(c => c.Messages)
                    .ThenInclude(m => m.ReadBy)
            .Include(u => u.Friendships)
                .ThenInclude(f => f.Friend)
            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

        if (currentUser == null)
        {
            return Unauthorized();
        }

        var userChats = currentUser.Chats
            .OrderByDescending(c => c.CreatedAt)
            .ToList();

        var unreadChatIds = userChats
            .Where(c => c.Messages.Any(m => !m.ReadBy.Any(u => u.Id == currentUser.Id)))
            .Select(c => c.Id)
            .ToHashSet();

        ViewBag.UnreadChatIds = unreadChatIds;
        ViewBag.Friends = currentUser.Friendships.Select(f => f.Friend).ToList();

        return View(userChats);
    }




    public async Task<IActionResult> Chat(int chatId)
    {
        var currentUser = await _context.Users
            .Include(u => u.ReadMessages)
            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

        if (currentUser == null) return Unauthorized();

        var chat = await _context.Chats
            .Include(c => c.Users)
            .Include(c => c.Messages)
                .ThenInclude(m => m.Sender)
            .FirstOrDefaultAsync(c => c.Id == chatId);

        if (chat == null) return NotFound("Chat not found.");

        foreach (var msg in chat.Messages)
        {
            if (!msg.ReadBy.Any(u => u.Id == currentUser.Id))
            {
                msg.ReadBy.Add(currentUser);
            }
        }

        await _context.SaveChangesAsync();

        return View(chat);
    }



    [HttpPost]
    public async Task<IActionResult> StartChat(string friendId)
    {
        var currentUser = await _context.Users
            .Include(u => u.Chats)
                .ThenInclude(c => c.Users)
            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

        if (currentUser == null)
        {
            return NotFound("Current user not found.");
        }

        var friend = await _context.Users
            .Include(u => u.Chats)
            .FirstOrDefaultAsync(u => u.Id == friendId);

        if (friend == null)
        {
            return NotFound("Friend not found.");
        }

        var oneOnOneChat = await _context.Chats
            .Include(c => c.Users)
            .Where(c => !c.isGroupchat &&
                        c.Users.Count == 2 &&
                        c.Users.Any(u => u.Id == currentUser.Id) &&
                        c.Users.Any(u => u.Id == friend.Id))
            .FirstOrDefaultAsync();

        if (oneOnOneChat != null)
        {
            return RedirectToAction("Chat", new { chatId = oneOnOneChat.Id });
        }

        var newChat = new Chat
        {
            Title = $"{currentUser.UserName} & {friend.UserName}",
            CreatedAt = DateTime.Now,
            isGroupchat = false,
            Status = "Active",
            Users = new List<User> { currentUser, friend }
        };

        _context.Chats.Add(newChat);
        await _context.SaveChangesAsync();

        return RedirectToAction("Chat", new { chatId = newChat.Id });
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

    [HttpPost]
    public async Task<IActionResult> AddUserToChat(int chatId, string userIdToAdd)
    {
        var currentUser = await _context.Users
            .Include(u => u.Chats)
            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

        if (currentUser == null)
        {
            return Unauthorized();
        }

        var chat = await _context.Chats
            .Include(c => c.Users)
            .FirstOrDefaultAsync(c => c.Id == chatId);

        if (chat == null || !chat.Users.Contains(currentUser))
        {
            return NotFound("Chat not found or unauthorized.");
        }

        var userToAdd = await _context.Users.FindAsync(userIdToAdd);

        if (userToAdd == null || chat.Users.Contains(userToAdd))
        {
            return BadRequest("User already in chat or invalid.");
        }

        chat.Users.Add(userToAdd);
        chat.isGroupchat = true;

        await _context.SaveChangesAsync();

        return RedirectToAction("Chat", new { chatId = chat.Id });
    }


}
