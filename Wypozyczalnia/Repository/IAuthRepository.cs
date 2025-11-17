using Wypozyczalnia.Models;

namespace Wypozyczalnia.Repository
{
    public interface IAuthRepository
    { 
        public Task Add(UserOneTimePassword oneTimePassword);
        public Task<UserOneTimePassword?> GetById(int id);
        public IQueryable<UserOneTimePassword?> GetAll();
        public Task Delete(int id);
    } }
