using Microsoft.AspNetCore.Identity;

namespace Wypozyczalnia.Validators.Password
{
    public class AllCharactersUniqueValidator<TUser>
        : IPasswordValidator<TUser> where TUser : IdentityUser
    {
        public Task<IdentityResult> 
            ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            bool isPasswordUnique=IsUnique(password);
            if (isPasswordUnique)
                return Task.FromResult(IdentityResult.Success);
            return Task.FromResult(IdentityResult.Failed(new IdentityError
            {
                Code = "AllCharactersUniqueValidator",
                Description = "Passwords must contain only unique characters"
            }));


        }
        private bool IsUnique(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;
            HashSet<char> seen = new HashSet<char>();
            foreach (char c in password)
            {
                if (!seen.Add(c))
                    return false;
            }
            return true;
        }
            
    }
}
