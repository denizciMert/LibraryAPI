using LibraryAPI.Entities.DTOs.AuthorDTO;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class AuthorMapper
	{
        public Author MapToEntity(AuthorPost dto)
        {
            var author = new Author
            {
                AuthorFullName = dto.AuthorName,
                Biography = dto.Biography,
                DateOfBirth = dto.DateOfBirth,
                DateOfDeath = dto.DateOfDeath
            };

            return author;
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

        public AuthorGet MapToDto(Author entity)
        {
            var dto = new AuthorGet
            {
                Id = entity.Id,
                AuthorName = entity.AuthorFullName,
                Biography = entity.Biography,
                DateOfBirth = entity.DateOfBirth,
                DateOfDeath = entity.DateOfDeath,
                Books = entity.AuthorBooks?.Select(ab => ab.Book.BookTitle).ToList(),
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
}
}

