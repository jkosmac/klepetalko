using System;
using System.Collections.Generic;

namespace klepetalko.Models;

public class Chat
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int UsersID { get; set; } //ustvaril
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool isGroupchat { get; set; }

    public ICollection<Message> Messages { get; set; }
    public ICollection<User> Users { get; set; }
}

