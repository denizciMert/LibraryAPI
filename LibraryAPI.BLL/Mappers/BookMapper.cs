using LibraryAPI.Entities.DTOs.BookDTO; // Importing the DTOs for Book
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Book entities and DTOs
    public class BookMapper
    {
        // Method to map BookPost DTO to Book entity
        public Book MapToEntity(BookPost dto)
        {
            var book = new Book
            {
                Isbn = dto.Isbn,
                BookTitle = dto.Title,
                PageCount = dto.PageCount,
                DateOfPublish = dto.DateOfPublish,
                CopyCount = (short)dto.CopyNumbers.Count,
                PublisherId = dto.PublisherId,
                LocationId = dto.LocationId
            };
            return CrossTables(book, dto);
        }

        // Method to handle cross-table mappings for Book entity
        public Book CrossTables(Book book, BookPost dto)
        {
            book.BookSubCategories = new List<BookSubCategory>();
            dto.SubCategoryIds.ForEach(s => book.BookSubCategories.Add(new BookSubCategory { SubCategoriesId = s, BooksId = book.Id }));

            book.BookLanguages = new List<BookLanguage>();
            dto.LanguageIds.ForEach(s => book.BookLanguages.Add(new BookLanguage { LanguagesId = s, BooksId = book.Id }));

            book.BookCopies = new List<BookCopy>();
            dto.CopyNumbers.ForEach(s => book.BookCopies.Add(new BookCopy { CopyNo = s, BookId = book.Id }));

            book.AuthorBooks = new List<AuthorBook>();
            dto.AuthorIds.ForEach(s => book.AuthorBooks.Add(new AuthorBook { AuthorsId = s, BooksId = book.Id }));

            return book;
        }

        // Method to map BookPost DTO to Book entity with additional fields
        public Book PostEntity(BookPost dto)
        {
            var book = new Book
            {
                Isbn = dto.Isbn,
                BookTitle = dto.Title,
                PageCount = dto.PageCount,
                DateOfPublish = dto.DateOfPublish,
                CopyCount = (short)dto.CopyNumbers.Count,
                PublisherId = dto.PublisherId,
                LocationId = dto.LocationId,
                BookSubCategories = dto.SubCategoryIds.Select(id => new BookSubCategory { SubCategoriesId = id }).ToList(),
                BookLanguages = dto.LanguageIds.Select(id => new BookLanguage { LanguagesId = id }).ToList(),
                AuthorBooks = dto.AuthorIds.Select(id => new AuthorBook { AuthorsId = id }).ToList(),
                BookCopies = dto.CopyNumbers.Select(id => new BookCopy { CopyNo = id, Reserved = false }).ToList(),
                BookImagePath = dto.ImagePath
            };
            return book;
        }

        // Method to update an existing Book entity with BookPost DTO data
        public Book UpdateEntity(Book book, BookPost bookPost)
        {
            book.Isbn = bookPost.Isbn;
            book.BookTitle = bookPost.Title;
            book.PageCount = bookPost.PageCount;
            book.DateOfPublish = bookPost.DateOfPublish;
            book.CopyCount = (short)bookPost.CopyNumbers.Count;
            book.PublisherId = bookPost.PublisherId;
            book.LocationId = bookPost.LocationId;
            book.BookSubCategories = new List<BookSubCategory>();
            bookPost.SubCategoryIds.ForEach(s => book.BookSubCategories.Add(new BookSubCategory { SubCategoriesId = s }));
            book.BookLanguages = new List<BookLanguage>();
            bookPost.LanguageIds.ForEach(s => book.BookLanguages.Add(new BookLanguage { LanguagesId = s }));
            book.BookCopies = new List<BookCopy>();
            bookPost.CopyNumbers.ForEach(s => book.BookCopies.Add(new BookCopy { CopyNo = s, Reserved = false }));
            book.AuthorBooks = new List<AuthorBook>();
            bookPost.AuthorIds.ForEach(s => book.AuthorBooks.Add(new AuthorBook { AuthorsId = s }));
            book.BookImagePath = bookPost.ImagePath;
            book.CreationDateLog = book.CreationDateLog;
            book.UpdateDateLog = DateTime.Now;
            book.DeleteDateLog = null;
            book.State = State.Güncellendi;

            return book;
        }

        // Method to mark a Book entity as deleted
        public Book DeleteEntity(Book book)
        {
            book.DeleteDateLog = DateTime.Now;
            book.State = State.Silindi;

            return book;
        }

        // Method to map Book entity to BookGet DTO
        public BookGet MapToDto(Book entity)
        {
            var dto = new BookGet
            {
                Id = entity.Id,
                Isbn = entity.Isbn,
                Title = entity.BookTitle,
                PageCount = entity.PageCount,
                DateOfPublish = entity.DateOfPublish,
                Publisher = entity.Publisher?.PublisherName,
                CopyCount = entity.CopyCount,
                Location = entity.Location?.ShelfCode,
                Rating = entity.Rating,
                Banned = entity.Banned,
                Authors = entity.AuthorBooks?.Select(ab => ab.Author!.AuthorFullName).ToList(),
                SubCategories = entity.BookSubCategories?.Select(bsc => bsc.SubCategory!.SubCategoryName).ToList(),
                Languages = entity.BookLanguages?.Select(bl => bl.Language!.LanguageName).ToList(),
                CopyNumbers = entity.BookCopies?.Select(bc => bc.CopyNo).ToList(),
                ImagePath = entity.BookImagePath,
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };
            return dto;
        }
    }
}
