using LibraryAPI.Entities.DTOs.AuthorDTO;
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

        public Author PostEntity(AuthorPost dto)
        {
            var author = new Author
            {
                AuthorFullName = dto.AuthorName,
                Biography = dto.Biography,
                DateOfBirth = dto.DateOfBirth,
                DateOfDeath = dto.DateOfDeath,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return author;
        }

        public Author UpdateEntity(Author author, AuthorPost authorPost)
        {
            author.AuthorFullName = authorPost.AuthorName;
            author.Biography = authorPost.Biography;
            author.DateOfBirth = authorPost.DateOfBirth;
            author.DateOfDeath = authorPost.DateOfDeath;
            author.CreationDateLog = author.CreationDateLog;
            author.UpdateDateLog = DateTime.Now;
            author.DeleteDateLog = null;
            author.State = State.Güncellendi;

            return author;
        }

        public Author DeleteEntity(Author author)
        {
            author.DeleteDateLog = DateTime.Now;
            author.State = State.Silindi;

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

