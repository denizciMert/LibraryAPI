using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.AuthorDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class AuthorService : ILibraryServiceManager<AuthorGet, AuthorPost, Author>
    {
        private readonly AuthorData _authorData;
        private readonly AuthorMapper _authorMapper;

        public AuthorService(ApplicationDbContext context)
        {
            _authorData = new AuthorData(context);
            _authorMapper = new AuthorMapper();
        }

        public async Task<ServiceResult<IEnumerable<AuthorGet>>> GetAllAsync()
        {
            try
            {
                var authors = await _authorData.SelectAllFiltered();
                if (authors == null || authors.Count == 0)
                {
                    return ServiceResult<IEnumerable<AuthorGet>>.FailureResult("Yazar verisi bulunmuyor.");
                }
                List<AuthorGet> authorGets = new List<AuthorGet>();
                foreach (var author in authors)
                {
                    var authorGet = _authorMapper.MapToDto(author);
                    authorGets.Add(authorGet);
                }
                return ServiceResult<IEnumerable<AuthorGet>>.SuccessResult(authorGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<AuthorGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<Author>>> GetAllWithDataAsync()
        {
            try
            {
                var authors = await _authorData.SelectAll();
                if (authors == null || authors.Count == 0)
                {
                    return ServiceResult<IEnumerable<Author>>.FailureResult("Yazar verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Author>>.SuccessResult(authors);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Author>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<AuthorGet>> GetByIdAsync(int id)
        {
            try
            {
                var author = await _authorData.SelectForEntity(id);
                if (author == null)
                {
                    return ServiceResult<AuthorGet>.FailureResult("Yazar verisi bulunmuyor.");
                }
                var authorGet = _authorMapper.MapToDto(author);
                return ServiceResult<AuthorGet>.SuccessResult(authorGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<AuthorGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<Author>> GetWithDataByIdAsync(int id)
        {
            try
            {
                var author = await _authorData.SelectForEntity(id);
                if (author == null)
                {
                    return ServiceResult<Author>.FailureResult("Yazar verisi bulunmuyor.");
                }
                return ServiceResult<Author>.SuccessResult(author);
            }
            catch (Exception ex)
            {
                return ServiceResult<Author>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<AuthorGet>> AddAsync(AuthorPost tPost)
        {
            try
            {
                if (await _authorData.IsRegistered(tPost))
                {
                    return ServiceResult<AuthorGet>.FailureResult("Bu yazar zaten eklenmiş.");
                }
                var newAuthor = _authorMapper.PostEntity(tPost);
                _authorData.AddToContext(newAuthor);
                await _authorData.SaveContext();
                var result = await GetByIdAsync(newAuthor.Id);
                return ServiceResult<AuthorGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<AuthorGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<AuthorGet>> UpdateAsync(int id, AuthorPost tPost)
        {
            try
            {
                var author = await _authorData.SelectForEntity(id);
                if (author == null)
                {
                    return ServiceResult<AuthorGet>.FailureResult("Yazar verisi bulunmuyor.");
                }
                _authorMapper.UpdateEntity(author, tPost);
                await _authorData.SaveContext();
                var newAuthor = _authorMapper.MapToDto(author);
                return ServiceResult<AuthorGet>.SuccessResult(newAuthor);
            }
            catch (Exception ex)
            {
                return ServiceResult<AuthorGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var author = await _authorData.SelectForEntity(id);
                if (author == null)
                {
                    return ServiceResult<bool>.FailureResult("Yazar verisi bulunmuyor.");
                }
                _authorMapper.DeleteEntity(author);
                await _authorData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }

}

