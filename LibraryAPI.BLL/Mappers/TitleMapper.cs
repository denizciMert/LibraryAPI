using LibraryAPI.Entities.DTOs.TitleDTO; // Importing DTOs related to Title
using LibraryAPI.Entities.Enums; // Importing enums used in the project
using LibraryAPI.Entities.Models; // Importing entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Title entities and DTOs
    public class TitleMapper
    {
        // Method to map TitlePost DTO to Title entity
        public Title MapToEntity(TitlePost dto)
        {
            var entity = new Title
            {
                TitleName = dto.TitleName
            };

            return entity;
        }

        // Method to map TitlePost DTO to Title entity with additional fields
        public Title PostEntity(TitlePost dto)
        {
            var title = new Title
            {
                TitleName = dto.TitleName,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return title;
        }

        // Method to update an existing Title entity with TitlePost DTO data
        public Title UpdateEntity(Title title, TitlePost titlePost)
        {
            title.TitleName = titlePost.TitleName;
            title.CreationDateLog = title.CreationDateLog;
            title.UpdateDateLog = DateTime.Now;
            title.DeleteDateLog = null;
            title.State = State.Güncellendi;

            return title;
        }

        // Method to mark a Title entity as deleted
        public Title DeleteEntity(Title title)
        {
            title.DeleteDateLog = DateTime.Now;
            title.State = State.Silindi;

            return title;
        }

        // Method to map Title entity to TitleGet DTO
        public TitleGet MapToDto(Title entity)
        {
            var dto = new TitleGet
            {
                Id = entity.Id,
                TitleName = entity.TitleName,
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
    }
}
