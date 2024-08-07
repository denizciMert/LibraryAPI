using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.AuthorDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// AuthorService class implements the ILibraryServiceManager interface and provides
    /// functionalities related to author management.
    /// </summary>
    public class AuthorService : ILibraryServiceManager<AuthorGet, AuthorPost, Author>
    {
        // Private fields to hold instances of data and mappers.
        private readonly AuthorData _authorData;
        private readonly AuthorMapper _authorMapper;

        /// <summary>
        /// Constructor to initialize the AuthorService with necessary dependencies.
        /// </summary>
        public AuthorService(ApplicationDbContext context)
        {
            _authorData = new AuthorData(context);
            _authorMapper = new AuthorMapper();
        }

        /// <summary>
        /// Retrieves all authors.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<AuthorGet>>> GetAllAsync()
        {
            try
            {
                var authors = await _authorData.SelectAllFiltered();
                if (authors.Count == 0)
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
                return ServiceResult<IEnumerable<AuthorGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all authors with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<Author>>> GetAllWithDataAsync()
        {
            try
            {
                var authors = await _authorData.SelectAll();
                if (authors.Count == 0)
                {
                    return ServiceResult<IEnumerable<Author>>.FailureResult("Yazar verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Author>>.SuccessResult(authors);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Author>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves an author by its ID.
        /// </summary>
        public async Task<ServiceResult<AuthorGet>> GetByIdAsync(int id)
        {
            try
            {
                Author? nullAuthor = null;
                var author = await _authorData.SelectForEntity(id);
                if (author == nullAuthor)
                {
                    return ServiceResult<AuthorGet>.FailureResult("Yazar verisi bulunmuyor.");
                }
                var authorGet = _authorMapper.MapToDto(author);
                return ServiceResult<AuthorGet>.SuccessResult(authorGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<AuthorGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves an author with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<Author>> GetWithDataByIdAsync(int id)
        {
            try
            {
                Author? nullAuthor = null;
                var author = await _authorData.SelectForEntity(id);
                if (author == nullAuthor)
                {
                    return ServiceResult<Author>.FailureResult("Yazar verisi bulunmuyor.");
                }
                return ServiceResult<Author>.SuccessResult(author);
            }
            catch (Exception ex)
            {
                return ServiceResult<Author>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new author.
        /// </summary>
        public async Task<ServiceResult<AuthorGet>> AddAsync(AuthorPost tPost)
        {
            try
            {
                if (await _authorData.IsRegistered(tPost))
                {
                    return ServiceResult<AuthorGet>.FailureResult("Bu yazar zaten eklenmiş.");
                }
                if (tPost.DateOfBirth > (DateTime.Now.Year - 4))
                {
                    return ServiceResult<AuthorGet>.FailureResult("Yazar 4 yaşından küçük olamaz.");
                }
                if (tPost.DateOfDeath < tPost.DateOfBirth + 4
                    || tPost.DateOfDeath > DateTime.Now.Year
                    || tPost.DateOfDeath - tPost.DateOfBirth > 123)
                {
                    return ServiceResult<AuthorGet>.FailureResult("Yazar ölüm yılı hatalı.");
                }
                var newAuthor = _authorMapper.PostEntity(tPost);
                _authorData.AddToContext(newAuthor);
                await _authorData.SaveContext();
                var result = await GetByIdAsync(newAuthor.Id);
                return ServiceResult<AuthorGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<AuthorGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing author.
        /// </summary>
        public async Task<ServiceResult<AuthorGet>> UpdateAsync(int id, AuthorPost tPost)
        {
            try
            {
                Author? nullAuthor = null;
                var author = await _authorData.SelectForEntity(id);
                if (author == nullAuthor)
                {
                    return ServiceResult<AuthorGet>.FailureResult("Yazar verisi bulunmuyor.");
                }

                if (author.AuthorFullName!=tPost.AuthorName)
                {
                    if (await _authorData.IsRegistered(tPost))
                    {
                        return ServiceResult<AuthorGet>.FailureResult("Bu yazar zaten eklenmiş.");
                    }
                }
                
                if (tPost.DateOfBirth > DateTime.Now.AddYears(-4).Year)
                {
                    return ServiceResult<AuthorGet>.FailureResult("Yazar 4 yaşından küçük olamaz.");
                }
                if (tPost.DateOfDeath < tPost.DateOfBirth && tPost.DateOfDeath > DateTime.Now.Year)
                {
                    return ServiceResult<AuthorGet>.FailureResult("Yazar ölüm yılı hatalı.");
                }
                _authorMapper.UpdateEntity(author, tPost);
                await _authorData.SaveContext();
                var newAuthor = _authorMapper.MapToDto(author);
                return ServiceResult<AuthorGet>.SuccessResult(newAuthor);
            }
            catch (Exception ex)
            {
                return ServiceResult<AuthorGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes an author by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                Author? nullAuthor = null;
                var author = await _authorData.SelectForEntity(id);
                if (author == nullAuthor)
                {
                    return ServiceResult<bool>.FailureResult("Yazar verisi bulunmuyor.");
                }
                _authorMapper.DeleteEntity(author);
                await _authorData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
