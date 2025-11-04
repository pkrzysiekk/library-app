namespace Wypozyczalnia.Services
{
    public interface IAuthService
    {
        public string GenerateOTP(string password);
        public Task AddOTP(string userId, string OTP);
    }
}
