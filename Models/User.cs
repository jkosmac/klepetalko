using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace klepetalko.Models;
public class User : IdentityUser
{
    public int Id { get; set; }
    public string? FirstName { get; set; } 
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<Friendship> Friendships { get; set; }
    public ICollection<Participant> Participants { get; set; }
    public ICollection<Message> Messages { get; set; }
}

    
