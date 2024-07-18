using LibraryAPI.Entities.DTOs.TitleDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class TitleMapper
	{
        public Title MapToEntity(TitlePost dto)
        {
            var entity = new Title
            {
                TitleName = dto.TitleName
            };

            return entity;
        }

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

