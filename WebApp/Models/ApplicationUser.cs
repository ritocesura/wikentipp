using Microsoft.AspNetCore.Identity;

namespace Wm22App.Models;

public class User : IdentityUser
{
    public string UserIcon { get; set; }
}