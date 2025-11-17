using Wypozyczalnia.Models;

namespace Wypozyczalnia.Services
{
    public interface IAuthService
    {
        public string GenerateOTP(string password);
        public Task AddOTP(string userId, string OTP);
        public Task RemoveOTP(string userId);
        public Task<UserOneTimePassword?> GetUserOtp(string userId);
    }
}
