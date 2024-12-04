using System;
using System.Collections.Generic;

namespace klepetalko.Models;

public class Participant
{
    public int Id { get; set; }

    public User User { get; set; }
    public Chat Chat { get; set; }
}