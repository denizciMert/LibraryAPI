using LibraryAPI.Entities.DTOs.BookDTO;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class BookMapper
	{
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
                LocationId = dto.LocationId,
                //Buradan sonrası kayıt oluşturulduktan sonra yapılmalı?
                BookSubCategories = dto.SubCategoryIds.Select(id => new BookSubCategory { SubCategoriesId = id }).ToList(),
                BookLanguages = dto.LanguageIds.Select(id => new BookLanguage { LanguagesId = id }).ToList(),
                AuthorBooks = dto.AuthorIds.Select(id => new AuthorBook { AuthorsId = id }).ToList(),
                BookCopies = dto.CopyNumbers.Select(id => new BookCopy { CopyNo = id}).ToList()
            };

            return book;
        }

        public Category PostEntity(CategoryPost dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return category;
        }

        public Category UpdateEntity(Category category, CategoryPost categoryPost)
        {
            category.CategoryName = categoryPost.CategoryName;
            category.CreationDateLog = category.CreationDateLog;
            category.UpdateDateLog = DateTime.Now;
            category.DeleteDateLog = null;
            category.State = State.Güncellendi;

            return category;
        }

        public Category DeleteEntity(Category category)
        {
            category.DeleteDateLog = DateTime.Now;
            category.State = State.Silindi;

            return category;
        }

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
                Authors = entity.AuthorBooks?.Select(ab => ab.Author.AuthorFullName).ToList(),
                SubCategories = entity.BookSubCategories?.Select(bsc => bsc.SubCategory.SubCategoryName).ToList(),
                Languages = entity.BookLanguages?.Select(bl => bl.Language.LanguageName).ToList(),
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
}
}

