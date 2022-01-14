using LibraryDAL;

namespace LibraryAPI
{
    public record Book(string Title, int ReleaseYear)
    {
        public Author Author { get; set; }

        public static Book FromBookEntity(BookEntity bookEntity)
            => new(bookEntity.Title, bookEntity.ReleaseYear)
            {
                Author = new(bookEntity.Author.FirstName, bookEntity.Author.LastName)
            };
    }
}
