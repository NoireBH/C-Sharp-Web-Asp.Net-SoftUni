using Library.Data;
using Library.Data.Entities;
using Library.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task AddBookAsync(AddBookViewModel bookModel)
        {
            var book = new Book()
            {
                Title = bookModel.Title,
                Author = bookModel.Author,
                Description = bookModel.Description,
                ImageUrl = bookModel.Url,
                Rating = bookModel.Rating,
                CategoryId = bookModel.CategoryId
            };

            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddBookToCollection(string userId, BookViewModel book)
        {
            bool isBookAlreadyAdded = await dbContext.IdentityUserBooks
                .AnyAsync(ub => ub.CollectorId == userId && ub.BookId == book.Id);

            if (!isBookAlreadyAdded)
            {
                var newBook = new IdentityUserBook
                {
                    CollectorId = userId,
                    BookId = book.Id,
                };

                await dbContext.IdentityUserBooks.AddAsync(newBook);
                await dbContext.SaveChangesAsync();
            }
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

        public async Task<BookViewModel?> GetBookByIdAsync(int id)
        {
            return await dbContext
                .Books
                .Where(b => b.Id == id)
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Rating = b.Rating,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    CategoryId = b.Category.Id
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BookMyBooksView>> GetMyBooksAsync(string userId)
        {
            return await dbContext
                .IdentityUserBooks
                .Where(ub => ub.CollectorId == userId)
                .Select(b => new BookMyBooksView()
                {
                    Id = b.Book.Id,
                    Title = b.Book.Title,
                    Author = b.Book.Author,
                    Description = b.Book.Description,
                    Category = b.Book.Category.Name,
                    ImageUrl = b.Book.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<AddBookViewModel> GetNewAddBookViewModelAsync()
        {
            var categories = await dbContext.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            var model = new AddBookViewModel() { Categories = categories };

            return model;
        }

        public async Task RemoveBookFromCollection(string userId, BookViewModel book)
        {
            var userBook = await dbContext
                    .IdentityUserBooks
                    .FirstOrDefaultAsync(ub => ub.CollectorId == userId && ub.Book.Id == book.Id);

            if (userBook != null)
            {
                dbContext.IdentityUserBooks.Remove(userBook);
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
