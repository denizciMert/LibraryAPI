using LibraryAPI.Entities.DTOs.LanguageDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class LanguageMapper
	{
        public Language MapToEntity(LanguagePost dto)
        {
            var language = new Language
            {
                LanguageName = dto.LanguageName,
                LanguageCode = dto.LanguageCode
            };

            return language;
        }

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

        public Language DeleteEntity(Language language)
        {
            language.DeleteDateLog = DateTime.Now;
            language.State = State.Silindi;

            return language;
        }

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

