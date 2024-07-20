using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.DTOs.StudyTableDTO;
using LibraryAPI.Entities.Enums;
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

        public StudyTable PostEntity(StudyTablePost dto)
        {
            var studyTable = new StudyTable()
            {
                TableCode = dto.TableCode,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return studyTable;
        }

        public StudyTable UpdateEntity(StudyTable studyTable, StudyTablePost studyTablePost)
        {
            studyTable.TableCode = studyTablePost.TableCode;
            studyTable.CreationDateLog = studyTable.CreationDateLog;
            studyTable.UpdateDateLog = DateTime.Now;
            studyTable.DeleteDateLog = null;
            studyTable.State = State.Güncellendi;

            return studyTable;
        }

        public StudyTable DeleteEntity(StudyTable studyTable)
        {
            studyTable.DeleteDateLog = DateTime.Now;
            studyTable.State = State.Silindi;

            return studyTable;
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

