using LibraryDAL;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LibraryAPI
{
    public static class LibraryDbContextHelper
    {
        public static BookEntity ToBookEntity(this Book book, LibraryDbContext context)
        {
            BookEntity bookEntity = new() { Title = book.Title, ReleaseYear = book.ReleaseYear };
            bookEntity.Author = context.Authors
                .FirstOrDefault(author => book.Author.FirstName == author.FirstName &&
                                          book.Author.LastName == author.LastName);

            return bookEntity;
        }

        public static AuthorEntity ToAuthorEntity(this Author author) =>
            new AuthorEntity() { FirstName = author.FirstName, LastName = author.LastName };
    }
}
