using Wypozyczalnia.Data;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Repository
{
    public class AuthorRepository : IAuthorRepository, IDisposable
    {
        private bool _disposed;
        private readonly LibraryContext _context;

        public AuthorRepository(LibraryContext context)
        {
            _context = context;
        }

        public void Delete(int authorId)
        {
            var authorToDelete = _context.Authors.Find(authorId);
            if (authorToDelete != null)
            {
                _context.Authors.Remove(authorToDelete);
            }
        }

        public IQueryable<Author> GetAll()
        {
            return _context.Authors;
        }

        public Author? GetById(int authorId)
        {
            return _context.Authors.Find(authorId);
        }

        public void Insert(Author author)
        {
            _context.Authors.Add(author);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Author author)
        {
            _context.Update(author);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}