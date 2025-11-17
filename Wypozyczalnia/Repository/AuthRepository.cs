using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly LibraryContext _ctx;
        public AuthRepository(LibraryContext context)
        {
            _ctx = context;
        }
        public async Task Add(UserOneTimePassword oneTimePassword)
        {
            await _ctx.AddAsync(oneTimePassword);
            await SaveChanges();
        } 

        public async Task Delete(int id)
        {
            var item = await GetById(id);
            if (item == null)
                return;
           _ctx.UserOneTimePasswords.Remove(item);
            await SaveChanges();
        }

        public IQueryable<UserOneTimePassword?> GetAll()
        {
            return _ctx.UserOneTimePasswords;
        }

        public async Task<UserOneTimePassword?> GetById(int id)
        {
           var item = await _ctx.UserOneTimePasswords.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }
        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
