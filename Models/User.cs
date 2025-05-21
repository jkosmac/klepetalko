using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace klepetalko.Models;
public class User : IdentityUser
{
    public string? FirstName { get; set; } 
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<Friendship> Friendships { get; set; } = new List<Friendship>();
    public ICollection<Chat> Chats { get; set; }
    public ICollection<Message> Messages { get; set; }
    public Setting? Setting { get; set; }
    public ICollection<Message> ReadMessages { get; set; } = new List<Message>();

}

    
