namespace LibraryAPI.Entities.DTOs.BookDTO
{
    public class BookGet
    {
        // The unique identifier for the book.
        public int Id { get; set; }

        // The International Standard Book Number (ISBN) of the book. This field is optional.
        public string? Isbn { get; set; } = string.Empty;

        // The title of the book. This field is optional.
        public string? Title { get; set; } = string.Empty;

        // The number of pages in the book. This field is optional.
        public short? PageCount { get; set; }

        // The year the book was published. This field is optional.
        public short? DateOfPublish { get; set; }

        // The publisher of the book. This field is optional.
        public string? Publisher { get; set; } = string.Empty;

        // The number of copies available for the book. This field is optional.
        public short? CopyCount { get; set; }

        // The location of the book. This field is optional.
        public string? Location { get; set; } = string.Empty;

        // The average rating of the book. This field is optional.
        public float? Rating { get; set; }

        // A list of authors associated with the book. This field is optional.
        public List<string>? Authors { get; set; } = [];

        // A list of subcategories the book belongs to. This field is optional.
        public List<string>? SubCategories { get; set; } = [];

        // A list of languages the book is available in. This field is optional.
        public List<string>? Languages { get; set; } = [];

        // A list of copy numbers for the book. This field is optional.
        public List<int>? CopyNumbers { get; set; }

        // The path to the book's image. This field is optional.
        public string? ImagePath { get; set; } = string.Empty;

        // Indicates if the book is banned. This field is optional.
        public bool? Banned { get; set; }

        // The date when the book record was created. This field is optional.
        public DateTime? CreatinDateLog { get; set; }

        // The date when the book record was last updated. This field is optional.
        public DateTime? UpdateDateLog { get; set; }

        // The date when the book record was deleted. This field is optional.
        public DateTime? DeleteDateLog { get; set; }

        // The state of the book record. This field is optional.
        public string? State { get; set; }
    }
}
