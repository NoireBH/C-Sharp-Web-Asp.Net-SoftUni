using Library.Models;

namespace Library.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<BookAllViewModel>> GetAllBooksAsync();
    }
}
