﻿using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.DAL;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.BLL.Services
{
    public class CategoryService : ILibraryServiceManager<CategoryGet,CategoryPost,Category>
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<IEnumerable<CategoryGet>>> GetAllAsync()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();

                if (categories == null || categories.Count == 0)
                {
                    return ServiceResult<IEnumerable<CategoryGet>>.FailureResult("Kategori verisi bulunmuyor.");
                }
                List<CategoryGet> categoryGets = new List<CategoryGet>();
                foreach (var category in categories)
                {
                    var categoryGet = new CategoryGet
                    {
                        Id = category.Id,
                        CategoryName = category.CategoryName,
                        State = category.State.ToString(),
                        CreatinDateLog = category.CreationDateLog,
                        UpdateDateLog = category.UpdateDateLog,
                        DeleteDateLog = category.DeleteDateLog
                    };
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
                var categories = await _context.Categories.ToListAsync();

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
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                {
                    return ServiceResult<CategoryGet>.FailureResult("Kategori verisi bulunmuyor.");
                }
                var categoryGet = new CategoryGet
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName,
                    State = category.State.ToString(),
                    CreatinDateLog = category.CreationDateLog,
                    UpdateDateLog = category.UpdateDateLog,
                    DeleteDateLog = category.DeleteDateLog
                };
                return ServiceResult<CategoryGet>.SuccessResult(categoryGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<CategoryGet>.FailureResult("Kategori verisi bulunmuyor.");
            }
        }

        public async Task<ServiceResult<Category>> GetWithDataByIdAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

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
                if ((await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == tPost.CategoryName)) != null)
                {
                    return ServiceResult<CategoryGet>.FailureResult("Bu kategori zaten eklenmiş.");
                }

                var newCategory = new Category
                {
                    CategoryName = tPost.CategoryName,
                    State = Entities.Enums.State.Eklendi,
                    CreationDateLog = DateTime.Now,
                    DeleteDateLog = null,
                    UpdateDateLog = null
                };
                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();

                var result = await GetByIdAsync(newCategory.Id);

                return ServiceResult<CategoryGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<CategoryGet>.FailureResult($"Kategori kaydı oluşturulurken bir hata meydana geldi. Hata: {ex.Message}");
            }
        }

        public async Task<ServiceResult<CategoryGet>> UpdateAsync(int id, CategoryPost tPost)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                {
                    return ServiceResult<CategoryGet>.FailureResult("Kategori verisi bulunmuyor.");
                }

                category.CategoryName = tPost.CategoryName;
                category.State = Entities.Enums.State.Güncellendi;
                category.UpdateDateLog = DateTime.Now;
                await _context.SaveChangesAsync();

                var newCategory = new CategoryGet
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName,
                    State = category.State.ToString(),
                    CreatinDateLog = category.CreationDateLog,
                    UpdateDateLog = category.UpdateDateLog,
                    DeleteDateLog = category.DeleteDateLog
                };

                return ServiceResult<CategoryGet>.SuccessResult(newCategory);
            }
            catch (Exception ex)
            {
                return ServiceResult<CategoryGet>.FailureResult($"Kategori verisi güncellenemedi. Hata: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                {
                    return ServiceResult<bool>.FailureResult("Kategori verisi bulunmuyor.");
                }
                category.DeleteDateLog = DateTime.Now;
                category.State = Entities.Enums.State.Silindi;
                await _context.SaveChangesAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Kategori verisi silinirken hata oluştu. Hata: {ex.Message}");
            }
        }
    }
}
