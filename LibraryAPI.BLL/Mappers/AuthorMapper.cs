using LibraryAPI.Entities.DTOs.AuthorDTO;
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

