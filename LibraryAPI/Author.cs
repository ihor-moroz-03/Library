using System.Collections.Generic;
using System.Linq;
using LibraryDAL;

namespace LibraryAPI
{
    public record Author(string FirstName, string LastName)
    {
        public IEnumerable<Book> Books { get; private init; }

        public static Author FromAuthorEntity(AuthorEntity authorEntity) =>
            new(authorEntity.FirstName, authorEntity.LastName)
            {
                Books = authorEntity.Books.Select(Book.FromBookEntity)
            };
    }
}
