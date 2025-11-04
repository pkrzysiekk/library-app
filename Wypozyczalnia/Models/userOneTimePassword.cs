using System.ComponentModel.DataAnnotations;

namespace Wypozyczalnia.Models
{
    public class UserOneTimePassword
    {
        public int Id { get; set; }
        public string UserId {  get; set; }
        public string OneTimePassword { get; set; }
    }
}
