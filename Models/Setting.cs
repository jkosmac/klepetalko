using System;
using System.Collections.Generic;

namespace klepetalko.Models;

public class Setting
{
    public int Id { get; set; }
    public bool darkMode { get; set; }
    public string UserId { get; set; }
    public User Userski { get; set; }
}