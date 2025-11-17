using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;
using Wypozyczalnia.Repository;

namespace Wypozyczalnia.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        public AuthService(IAuthRepository repository)
        {
            _repository = repository;
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
            var prevOtp = _repository
                .GetAll()
                .Where(x => x.UserId == userId)
                .AsEnumerable();

            if (prevOtp != null)
            {
                foreach (var otp in prevOtp)
                {
                    await _repository.Delete(otp.Id);
                }
            }
            var userPassword = new UserOneTimePassword()
            {
                UserId = userId,
                OneTimePassword = OTP
            };

            await _repository.Add(userPassword);
        }

        public async Task<UserOneTimePassword?> GetUserOtp(string userId)
        {
            var userOtp = await _repository
                .GetAll()
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();
                return userOtp;   
        }

        public async Task RemoveOTP(string userId)
        {
            var otpToDelete= await _repository
                .GetAll()
                .Where(x=>x.UserId == userId)
                .FirstOrDefaultAsync();
            if (otpToDelete != null)
                await _repository.Delete(otpToDelete.Id);
        }
    }
}
