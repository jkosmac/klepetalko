using System;
using System.Collections.Generic;

namespace klepetalko.Models;
public class Message
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string? Attachment { get; set; }
    public DateTime TimeCreated { get; set; }

    public ICollection<User> ReadBy { get; set; } = new List<User>();
    public User Sender { get; set; }
    public Chat Chat { get; set; }
}

