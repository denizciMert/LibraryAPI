using LibraryAPI.Entities.DTOs.TitleDTO;
using LibraryAPI.Entities.Enums;
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

        public Title UpdateEntity(Title title, TitlePost titlePost)
        {
            title.TitleName = titlePost.TitleName;
            title.CreationDateLog = title.CreationDateLog;
            title.UpdateDateLog = DateTime.Now;
            title.DeleteDateLog = null;
            title.State = State.Güncellendi;

            return title;
        }

        public Title DeleteEntity(Title title)
        {
            title.DeleteDateLog = DateTime.Now;
            title.State = State.Silindi;

            return title;
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

