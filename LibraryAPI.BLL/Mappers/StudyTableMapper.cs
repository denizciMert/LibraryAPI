using LibraryAPI.Entities.DTOs.StudyTableDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class StudyTableMapper
	{
        public StudyTable MapToEntity(StudyTablePost studyTablePost)
        {
            var entity = new StudyTable
            {
                TableCode = studyTablePost.TableCode
            };

            return entity;
        }

        public StudyTableGet MapToDto(StudyTable studyTable)
        {
            var dto = new StudyTableGet
            {
                Id = studyTable.Id,
                TableCode = studyTable.TableCode,
                CreatinDateLog = studyTable.CreationDateLog,
                UpdateDateLog = studyTable.UpdateDateLog,
                DeleteDateLog = studyTable.DeleteDateLog,
                State = studyTable.State.ToString()
            };

            return dto;
        }
    }
}

