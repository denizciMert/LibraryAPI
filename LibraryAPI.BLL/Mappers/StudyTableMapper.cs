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

        public Category PostEntity(CategoryPost dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return category;
        }

        public Category UpdateEntity(Category category, CategoryPost categoryPost)
        {
            category.CategoryName = categoryPost.CategoryName;
            category.CreationDateLog = category.CreationDateLog;
            category.UpdateDateLog = DateTime.Now;
            category.DeleteDateLog = null;
            category.State = State.Güncellendi;

            return category;
        }

        public Category DeleteEntity(Category category)
        {
            category.DeleteDateLog = DateTime.Now;
            category.State = State.Silindi;

            return category;
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

