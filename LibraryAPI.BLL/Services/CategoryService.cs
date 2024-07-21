using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class CategoryService : ILibraryServiceManager<CategoryGet,CategoryPost,Category>
    {
        private readonly CategoryData _categoryData;
        private readonly CategoryMapper _categoryMapper;

        public CategoryService(ApplicationDbContext context)
        {
            _categoryData = new CategoryData(context);
            _categoryMapper = new CategoryMapper();
        }

        public async Task<ServiceResult<IEnumerable<CategoryGet>>> GetAllAsync()
        {
            try
            {
                var categories = await _categoryData.SelectAllFiltered();
                if (categories == null || categories.Count == 0)
                {
                    return ServiceResult<IEnumerable<CategoryGet>>.FailureResult("Kategori verisi bulunmuyor.");
                }
                List<CategoryGet> categoryGets = new List<CategoryGet>();
                foreach (var category in categories)
                {
                    var categoryGet = _categoryMapper.MapToDto(category);
                    categoryGets.Add(categoryGet);
                }
                return ServiceResult<IEnumerable<CategoryGet>>.SuccessResult(categoryGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<CategoryGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<Category>>> GetAllWithDataAsync()
        {
            try
            {
                var categories = await _categoryData.SelectAll();
                if (categories == null || categories.Count == 0)
                {
                    return ServiceResult<IEnumerable<Category>>.FailureResult("Kategori verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Category>>.SuccessResult(categories);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Category>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<CategoryGet>> GetByIdAsync(int id)
        {
            try
            {
                var category = await _categoryData.SelectForEntity(id);
                if (category == null)
                {
                    return ServiceResult<CategoryGet>.FailureResult("Kategori verisi bulunmuyor.");
                }
                var categoryGet = _categoryMapper.MapToDto(category);
                return ServiceResult<CategoryGet>.SuccessResult(categoryGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<CategoryGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<Category>> GetWithDataByIdAsync(int id)
        {
            try
            {
                var category = await _categoryData.SelectForEntity(id);
                if (category == null)
                {
                    return ServiceResult<Category>.FailureResult("Kategori verisi bulunmuyor.");
                }
                return ServiceResult<Category>.SuccessResult(category);
            }
            catch (Exception ex)
            {
                return ServiceResult<Category>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<CategoryGet>> AddAsync(CategoryPost tPost)
        {
            try
            {
                if ((await _categoryData.SelectForEntityName(tPost.CategoryName)) != null)
                {
                    return ServiceResult<CategoryGet>.FailureResult("Bu kategori zaten eklenmiş.");
                }
                var newCategory = _categoryMapper.PostEntity(tPost);
                _categoryData.AddToContext(newCategory);
                await _categoryData.SaveContext();
                var result = await GetByIdAsync(newCategory.Id);
                return ServiceResult<CategoryGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<CategoryGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<CategoryGet>> UpdateAsync(int id, CategoryPost tPost)
        {
            try
            {
                var category = await _categoryData.SelectForEntity(id);
                if (category == null)
                {
                    return ServiceResult<CategoryGet>.FailureResult("Kategori verisi bulunmuyor.");
                }
                _categoryMapper.UpdateEntity(category, tPost);
                await _categoryData.SaveContext();
                var newCategory = _categoryMapper.MapToDto(category);
                return ServiceResult<CategoryGet>.SuccessResult(newCategory);
            }
            catch (Exception ex)
            {
                return ServiceResult<CategoryGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var category = await _categoryData.SelectForEntity(id);
                if (category == null)
                {
                    return ServiceResult<bool>.FailureResult("Kategori verisi bulunmuyor.");
                }
                _categoryMapper.DeleteEntity(category);
                await _categoryData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
