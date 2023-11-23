using Library.Models;

namespace Library.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<BookAllViewModel>> GetAllBooksAsync();

        public Task<IEnumerable<BookMyBooksView>> GetMyBooksAsync(string userId);

        public Task<BookViewModel?> GetBookByIdAsync(int id);

        public Task AddBookToCollection(string userId, BookViewModel book);

        public Task RemoveBookFromCollection(string userId, BookViewModel book);
        public Task<AddBookViewModel> GetNewAddBookViewModelAsync();
        public Task AddBookAsync(AddBookViewModel bookModel);
    }
}
