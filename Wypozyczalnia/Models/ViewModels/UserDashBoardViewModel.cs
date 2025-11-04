using Microsoft.AspNetCore.Identity;

namespace Wypozyczalnia.Models.ViewModels
{
    public class UserDashBoardViewModel
    {
        public IdentityUser User { get; set; }
        public string? Otp {  get; set; }
    }
}
