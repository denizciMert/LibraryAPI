using System;
using LibraryAPI.Entities.DTOs.LanguageDTO;
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

