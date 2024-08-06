using LibraryAPI.Entities.DTOs.StudyTableDTO; // Importing DTOs related to StudyTable
using LibraryAPI.Entities.Enums; // Importing enums used in the project
using LibraryAPI.Entities.Models; // Importing entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between StudyTable entities and DTOs
    public class StudyTableMapper
    {
        // Method to map StudyTablePost DTO to StudyTable entity
        public StudyTable MapToEntity(StudyTablePost studyTablePost)
        {
            var entity = new StudyTable
            {
                TableCode = studyTablePost.TableCode
            };

            return entity;
        }

        // Method to map StudyTablePost DTO to StudyTable entity with additional fields
        public StudyTable PostEntity(StudyTablePost dto)
        {
            var studyTable = new StudyTable
            {
                TableCode = dto.TableCode,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return studyTable;
        }

        // Method to update an existing StudyTable entity with StudyTablePost DTO data
        public StudyTable UpdateEntity(StudyTable studyTable, StudyTablePost studyTablePost)
        {
            studyTable.TableCode = studyTablePost.TableCode;
            studyTable.CreationDateLog = studyTable.CreationDateLog;
            studyTable.UpdateDateLog = DateTime.Now;
            studyTable.DeleteDateLog = null;
            studyTable.State = State.Güncellendi;

            return studyTable;
        }

        // Method to mark a StudyTable entity as deleted
        public StudyTable DeleteEntity(StudyTable studyTable)
        {
            studyTable.DeleteDateLog = DateTime.Now;
            studyTable.State = State.Silindi;

            return studyTable;
        }

        // Method to map StudyTable entity to StudyTableGet DTO
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
