using System;
using System.Collections.Generic;

namespace klepetalko.Models;

public class Friendship
{
    public int Id { get; set; }
    public DateTime FriendshipTime { get; set; }

    public User Friend { get; set; }
}
