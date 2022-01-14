using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDAL
{
    public class BookEntity
    {
        public long Id { get; set; }
        public string Title { get; set; }

        [NotMapped]
        public string AuthorName => Author != null ? $"{Author.FirstName} {Author.LastName}" : "n/a";

        public int ReleaseYear { get; set; }

        [JsonIgnore]
        public AuthorEntity Author { get; set; }
    }
}
