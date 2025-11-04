using Wypozyczalnia.Data;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Services
{
    public class AuthService : IAuthService
    {
        private readonly LibraryContext _libraryContext;
        public AuthService(LibraryContext ctx)
        {
            _libraryContext = ctx;
        }

        public string GenerateOTP(string password)
        {
            int passwordLength=password.Length;
            int randomNumber = Random.Shared.Next(1, 101);
            double result= (passwordLength * Math.Sin(randomNumber));
            return Math.Round(result,10).ToString(); 
        }

        public async Task AddOTP(string userId, string OTP)
        {
            var userPassword = new UserOneTimePassword()
            {
                UserId = userId,
                OneTimePassword = OTP
            };

            await _libraryContext.UserOneTimePasswords.AddAsync(userPassword);
            await _libraryContext.SaveChangesAsync();

        }

    }
}
