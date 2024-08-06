using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// CategoryService class implements the ILibraryServiceManager interface and provides
    /// functionalities related to category management.
    /// </summary>
    public class CategoryService : ILibraryServiceManager<CategoryGet, CategoryPost, Category>
    {
        // Private fields to hold instances of data and mappers.
        private readonly CategoryData _categoryData;
        private readonly CategoryMapper _categoryMapper;

        /// <summary>
        /// Constructor to initialize the CategoryService with necessary dependencies.
        /// </summary>
        public CategoryService(ApplicationDbContext context)
        {
            _categoryData = new CategoryData(context);
            _categoryMapper = new CategoryMapper();
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<CategoryGet>>> GetAllAsync()
        {
            try
            {
                var categories = await _categoryData.SelectAllFiltered();
                if (categories.Count == 0)
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
                return ServiceResult<IEnumerable<CategoryGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all categories with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<Category>>> GetAllWithDataAsync()
        {
            try
            {
                var categories = await _categoryData.SelectAll();
                if (categories.Count == 0)
                {
                    return ServiceResult<IEnumerable<Category>>.FailureResult("Kategori verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Category>>.SuccessResult(categories);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Category>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        public async Task<ServiceResult<CategoryGet>> GetByIdAsync(int id)
        {
            try
            {
                Category? nullCategory = null;
                var category = await _categoryData.SelectForEntity(id);
                if (category == nullCategory)
                {
                    return ServiceResult<CategoryGet>.FailureResult("Kategori verisi bulunmuyor.");
                }
                var categoryGet = _categoryMapper.MapToDto(category);
                return ServiceResult<CategoryGet>.SuccessResult(categoryGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<CategoryGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a category with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<Category>> GetWithDataByIdAsync(int id)
        {
            try
            {
                Category? nullCategory = null;
                var category = await _categoryData.SelectForEntity(id);
                if (category == nullCategory)
                {
                    return ServiceResult<Category>.FailureResult("Kategori verisi bulunmuyor.");
                }
                return ServiceResult<Category>.SuccessResult(category);
            }
            catch (Exception ex)
            {
                return ServiceResult<Category>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
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
                return ServiceResult<CategoryGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        public async Task<ServiceResult<CategoryGet>> UpdateAsync(int id, CategoryPost tPost)
        {
            try
            {
                Category? nullCategory = null;
                var category = await _categoryData.SelectForEntity(id);
                if (category == nullCategory)
                {
                    return ServiceResult<CategoryGet>.FailureResult("Kategori verisi bulunmuyor.");
                }
                if ((await _categoryData.SelectForEntityName(tPost.CategoryName)) != null)
                {
                    return ServiceResult<CategoryGet>.FailureResult("Bu kategori zaten eklenmiş.");
                }
                _categoryMapper.UpdateEntity(category, tPost);
                await _categoryData.SaveContext();
                var newCategory = _categoryMapper.MapToDto(category);
                return ServiceResult<CategoryGet>.SuccessResult(newCategory);
            }
            catch (Exception ex)
            {
                return ServiceResult<CategoryGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                Category? nullCategory = null;
                var category = await _categoryData.SelectForEntity(id);
                if (category == nullCategory)
                {
                    return ServiceResult<bool>.FailureResult("Kategori verisi bulunmuyor.");
                }
                _categoryMapper.DeleteEntity(category);
                await _categoryData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
