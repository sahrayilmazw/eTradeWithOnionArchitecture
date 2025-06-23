using Microsoft.AspNetCore.Identity;

namespace eTradeWithOnionArchitecture.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string? Fullname { get; set; }
    }
}
