using LibraryAPI.Entities.DTOs.AuthorDTO; // Importing the DTOs for Author
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Author entities and DTOs
    public class AuthorMapper
    {
        // Method to map AuthorPost DTO to Author entity
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

        // Method to map AuthorPost DTO to Author entity with additional fields
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

        // Method to update an existing Author entity with AuthorPost DTO data
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

        // Method to mark an Author entity as deleted
        public Author DeleteEntity(Author author)
        {
            author.DeleteDateLog = DateTime.Now;
            author.State = State.Silindi;

            return author;
        }

        // Method to map Author entity to AuthorGet DTO
        public AuthorGet MapToDto(Author entity)
        {
            var dto = new AuthorGet
            {
                Id = entity.Id,
                AuthorName = entity.AuthorFullName,
                Biography = entity.Biography,
                DateOfBirth = entity.DateOfBirth,
                DateOfDeath = entity.DateOfDeath,
                Books = entity.AuthorBooks?.Select(ab => ab.Book!.BookTitle).ToList(),
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
    }
}
