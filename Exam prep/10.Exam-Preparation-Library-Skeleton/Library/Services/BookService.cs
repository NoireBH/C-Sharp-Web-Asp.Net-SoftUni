using Library.Data;
using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<BookAllViewModel>> GetAllBooksAsync()
        {
            return await dbContext
                .Books.Select(b => new BookAllViewModel()
                {
                    Id = b.Id,
                    ImageUrl = b.ImageUrl,
                    Title = b.Title,
                    Author = b.Author,
                    Rating = b.Rating,
                    Category = b.Category.Name
                }).ToListAsync();
        }
    }
}
