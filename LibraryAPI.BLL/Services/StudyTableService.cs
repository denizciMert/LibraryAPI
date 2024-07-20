using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.StudyTableDTO;
using LibraryAPI.Entities.DTOs.SubCategoryDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class StudyTableService : ILibraryServiceManager<StudyTableGet,StudyTablePost,StudyTable>
    {
        private readonly StudyTableData _studyTableData;
        private readonly StudyTableMapper _studyTableMapper;

        public StudyTableService(ApplicationDbContext context)
        {
            _studyTableData = new StudyTableData(context);
            _studyTableMapper = new StudyTableMapper();
        }

        public async Task<ServiceResult<IEnumerable<StudyTableGet>>> GetAllAsync()
        {
            try
            {
                var studyTables = await _studyTableData.SelectAll();
                if (studyTables == null || studyTables.Count == 0)
                {
                    return ServiceResult<IEnumerable<StudyTableGet>>.FailureResult("Çalışma tablosu verisi bulunmuyor.");
                }
                List<StudyTableGet> studyTableGets = new List<StudyTableGet>();
                foreach (var studyTable in studyTables)
                {
                    var studyTableGet = _studyTableMapper.MapToDto(studyTable);
                    studyTableGets.Add(studyTableGet);
                }
                return ServiceResult<IEnumerable<StudyTableGet>>.SuccessResult(studyTableGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<StudyTableGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<StudyTable>>> GetAllWithDataAsync()
        {
            try
            {
                var studyTables = await _studyTableData.SelectAll();
                if (studyTables == null || studyTables.Count == 0)
                {
                    return ServiceResult<IEnumerable<StudyTable>>.FailureResult("Çalışma tablosu verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<StudyTable>>.SuccessResult(studyTables);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<StudyTable>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<StudyTableGet>> GetByIdAsync(int id)
        {
            try
            {
                var studyTable = await _studyTableData.SelectForEntity(id);
                if (studyTable == null)
                {
                    return ServiceResult<StudyTableGet>.FailureResult("Çalışma tablosu verisi bulunmuyor.");
                }
                var studyTableGet = _studyTableMapper.MapToDto(studyTable);
                return ServiceResult<StudyTableGet>.SuccessResult(studyTableGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<StudyTableGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<StudyTable>> GetWithDataByIdAsync(int id)
        {
            try
            {
                var studyTable = await _studyTableData.SelectForEntity(id);
                if (studyTable == null)
                {
                    return ServiceResult<StudyTable>.FailureResult("Çalışma tablosu verisi bulunmuyor.");
                }
                return ServiceResult<StudyTable>.SuccessResult(studyTable);
            }
            catch (Exception ex)
            {
                return ServiceResult<StudyTable>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<StudyTableGet>> AddAsync(StudyTablePost tPost)
        {
            try
            {
                if (await _studyTableData.IsRegistered(tPost))
                {
                    return ServiceResult<StudyTableGet>.FailureResult("Bu çalışma tablosu zaten eklenmiş.");
                }
                var newStudyTable = _studyTableMapper.PostEntity(tPost);
                _studyTableData.AddToContext(newStudyTable);
                await _studyTableData.SaveContext();
                var result = await GetByIdAsync(newStudyTable.Id);
                return ServiceResult<StudyTableGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<StudyTableGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<StudyTableGet>> UpdateAsync(int id, StudyTablePost tPost)
        {
            try
            {
                var studyTable = await _studyTableData.SelectForEntity(id);
                if (studyTable == null)
                {
                    return ServiceResult<StudyTableGet>.FailureResult("Çalışma tablosu verisi bulunmuyor.");
                }
                _studyTableMapper.UpdateEntity(studyTable, tPost);
                await _studyTableData.SaveContext();
                var updatedStudyTable = _studyTableMapper.MapToDto(studyTable);
                return ServiceResult<StudyTableGet>.SuccessResult(updatedStudyTable);
            }
            catch (Exception ex)
            {
                return ServiceResult<StudyTableGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var studyTable = await _studyTableData.SelectForEntity(id);
                if (studyTable == null)
                {
                    return ServiceResult<bool>.FailureResult("Çalışma tablosu verisi bulunmuyor.");
                }
                _studyTableMapper.DeleteEntity(studyTable);
                await _studyTableData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
