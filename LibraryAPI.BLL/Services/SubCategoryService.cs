﻿using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.SubCategoryDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// Service class for managing SubCategory operations
    /// </summary>
    public class SubCategoryService : ILibraryServiceManager<SubCategoryGet,SubCategoryPost,SubCategory>
    {
        private readonly SubCategoryData _subCategoryData;
        private readonly SubCategoryMapper _subCategoryMapper;

        /// <summary>
        /// Private fields for data access and mapping
        /// </summary>
        public SubCategoryService(ApplicationDbContext context)
        {
            _subCategoryData = new SubCategoryData(context);
            _subCategoryMapper = new SubCategoryMapper();
        }

        /// <summary>
        /// Method to get all subcategories
        /// </summary>
        public async Task<ServiceResult<IEnumerable<SubCategoryGet>>> GetAllAsync()
        {
            try
            {
                var subCategories = await _subCategoryData.SelectAllFiltered();
                if (subCategories.Count == 0)
                {
                    return ServiceResult<IEnumerable<SubCategoryGet>>.FailureResult("Alt kategori verisi bulunmuyor.");
                }
                List<SubCategoryGet> subCategoryGets = new List<SubCategoryGet>();
                foreach (var subCategory in subCategories)
                {
                    var subCategoryGet = _subCategoryMapper.MapToDto(subCategory);
                    subCategoryGets.Add(subCategoryGet);
                }
                return ServiceResult<IEnumerable<SubCategoryGet>>.SuccessResult(subCategoryGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<SubCategoryGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to get all subcategories with data
        /// </summary>
        public async Task<ServiceResult<IEnumerable<SubCategory>>> GetAllWithDataAsync()
        {
            try
            {
                var subCategories = await _subCategoryData.SelectAll();
                if (subCategories.Count == 0)
                {
                    return ServiceResult<IEnumerable<SubCategory>>.FailureResult("Alt kategori verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<SubCategory>>.SuccessResult(subCategories);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<SubCategory>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to get a subcategory by ID
        /// </summary>
        public async Task<ServiceResult<SubCategoryGet>> GetByIdAsync(int id)
        {
            try
            {
                SubCategory? nullSubCategory = null;
                var subCategory = await _subCategoryData.SelectForEntity(id);
                if (subCategory == nullSubCategory)
                {
                    return ServiceResult<SubCategoryGet>.FailureResult("Alt kategori verisi bulunmuyor.");
                }
                var subCategoryGet = _subCategoryMapper.MapToDto(subCategory);
                return ServiceResult<SubCategoryGet>.SuccessResult(subCategoryGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<SubCategoryGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to get a subcategory with data by ID
        /// </summary>
        public async Task<ServiceResult<SubCategory>> GetWithDataByIdAsync(int id)
        {
            try
            {
                SubCategory? nullSubCategory = null;
                var subCategory = await _subCategoryData.SelectForEntity(id);
                if (subCategory == nullSubCategory)
                {
                    return ServiceResult<SubCategory>.FailureResult("Alt kategori verisi bulunmuyor.");
                }
                return ServiceResult<SubCategory>.SuccessResult(subCategory);
            }
            catch (Exception ex)
            {
                return ServiceResult<SubCategory>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to add a new subcategory
        /// </summary>
        public async Task<ServiceResult<SubCategoryGet>> AddAsync(SubCategoryPost tPost)
        {
            try
            {
                if (await _subCategoryData.IsRegistered(tPost))
                {
                    return ServiceResult<SubCategoryGet>.FailureResult("Bu alt kategori zaten eklenmiş.");
                }
                var newSubCategory = _subCategoryMapper.PostEntity(tPost);
                _subCategoryData.AddToContext(newSubCategory);
                await _subCategoryData.SaveContext();
                var result = await GetByIdAsync(newSubCategory.Id);
                return ServiceResult<SubCategoryGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<SubCategoryGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to update an existing subcategory
        /// </summary>
        public async Task<ServiceResult<SubCategoryGet>> UpdateAsync(int id, SubCategoryPost tPost)
        {
            try
            {
                if (await _subCategoryData.IsRegistered(tPost))
                {
                    return ServiceResult<SubCategoryGet>.FailureResult("Bu alt kategori zaten eklenmiş.");
                }
                SubCategory? nullSubCategory = null;
                var subCategory = await _subCategoryData.SelectForEntity(id);
                if (subCategory == nullSubCategory)
                {
                    return ServiceResult<SubCategoryGet>.FailureResult("Alt kategori verisi bulunmuyor.");
                }
                _subCategoryMapper.UpdateEntity(subCategory, tPost);
                await _subCategoryData.SaveContext();
                var updatedSubCategory = _subCategoryMapper.MapToDto(subCategory);
                return ServiceResult<SubCategoryGet>.SuccessResult(updatedSubCategory);
            }
            catch (Exception ex)
            {
                return ServiceResult<SubCategoryGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to delete a subcategory by ID
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                SubCategory? nullSubCategory = null;
                var subCategory = await _subCategoryData.SelectForEntity(id);
                if (subCategory == nullSubCategory)
                {
                    return ServiceResult<bool>.FailureResult("Alt kategori verisi bulunmuyor.");
                }
                _subCategoryMapper.DeleteEntity(subCategory);
                await _subCategoryData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
