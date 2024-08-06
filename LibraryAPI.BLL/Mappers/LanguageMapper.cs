using LibraryAPI.Entities.DTOs.LanguageDTO; // Importing the DTOs for Language
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Language entities and DTOs
    public class LanguageMapper
    {
        // Method to map LanguagePost DTO to Language entity
        public Language MapToEntity(LanguagePost dto)
        {
            var language = new Language
            {
                LanguageName = dto.LanguageName,
                LanguageCode = dto.LanguageCode
            };

            return language;
        }

        // Method to map LanguagePost DTO to Language entity with additional fields
        public Language PostEntity(LanguagePost dto)
        {
            var language = new Language
            {
                LanguageCode = dto.LanguageCode,
                LanguageName = dto.LanguageName,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return language;
        }

        // Method to update an existing Language entity with LanguagePost DTO data
        public Language UpdateEntity(Language language, LanguagePost languagePost)
        {
            language.LanguageCode = languagePost.LanguageCode;
            language.LanguageName = languagePost.LanguageName;
            language.CreationDateLog = language.CreationDateLog;
            language.UpdateDateLog = DateTime.Now;
            language.DeleteDateLog = null;
            language.State = State.Güncellendi;

            return language;
        }

        // Method to mark a Language entity as deleted
        public Language DeleteEntity(Language language)
        {
            language.DeleteDateLog = DateTime.Now;
            language.State = State.Silindi;

            return language;
        }

        // Method to map Language entity to LanguageGet DTO
        public LanguageGet MapToDto(Language entity)
        {
            var dto = new LanguageGet
            {
                Id = entity.Id,
                LanguageName = entity.LanguageName,
                LanguageCode = entity.LanguageCode,
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
    }
}
