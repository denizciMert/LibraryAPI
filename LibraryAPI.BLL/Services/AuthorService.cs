using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.AuthorDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class AuthorService : ILibraryServiceManager<AuthorGet,AuthorPost,Author>
    {
        private readonly AuthorData _authorData;

        public AuthorService(AuthorData authorData)
        {
            _authorData = authorData;
        }

        public async Task<ServiceResult<IEnumerable<AuthorGet>>> GetAllAsync()
        {
            try
            {
                var authors = await _authorData.SelectAll();

                if (authors == null || authors.Count == 0)
                {
                    return ServiceResult<IEnumerable<AuthorGet>>.FailureResult("Yazar verisi bulunmuyor.");
                }
                List<AuthorGet> authorGets = new List<AuthorGet>();
                foreach (var author in authors)
                {
                    var books = await _authorData.SelectBooks(author.Id);
                    var authorGet = new AuthorGet
                    {
                        Id = author.Id,
                        AuthorName = author.AuthorFullName,
                        Biography = author.Biography,
                        DateOfBirth = author.DateOfBirth,
                        DateOfDeath = author.DateOfDeath,
                        Books = books,
                        State = author.State.ToString(),
                        CreatinDateLog = author.CreationDateLog,
                        UpdateDateLog = author.UpdateDateLog,
                        DeleteDateLog = author.DeleteDateLog
                    };
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
                var books = await _authorData.SelectBooks(author.Id);
                var authorGet = new AuthorGet
                {
                    Id = author.Id,
                    AuthorName = author.AuthorFullName,
                    Biography = author.Biography,
                    DateOfBirth = author.DateOfBirth,
                    DateOfDeath = author.DateOfDeath,
                    Books = books,
                    State = author.State.ToString(),
                    CreatinDateLog = author.CreationDateLog,
                    UpdateDateLog = author.UpdateDateLog,
                    DeleteDateLog = author.DeleteDateLog
                };
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

                var newAuthor = new Author
                {
                    AuthorFullName = tPost.AuthorName,
                    Biography = tPost.Biography,
                    DateOfBirth = tPost.DateOfBirth,
                    DateOfDeath = tPost.DateOfDeath,
                    State = State.Eklendi,
                    CreationDateLog = DateTime.Now,
                    DeleteDateLog = null,
                    UpdateDateLog = null
                };
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

                author.AuthorFullName = tPost.AuthorName;
                author.Biography = tPost.Biography;
                author.DateOfBirth = tPost.DateOfBirth;
                author.DateOfDeath = tPost.DateOfDeath;
                author.State = Entities.Enums.State.Güncellendi;
                author.UpdateDateLog = DateTime.Now;

                await _authorData.SaveContext();

                var newAuthor = new AuthorGet
                {
                    Id = author.Id,
                    AuthorName = author.AuthorFullName,
                    Biography = author.Biography,
                    DateOfBirth = author.DateOfBirth,
                    DateOfDeath = author.DateOfDeath,
                    Books = null,
                    State = State.Eklendi.ToString(),
                    CreatinDateLog = author.CreationDateLog,
                    UpdateDateLog = author.UpdateDateLog,
                    DeleteDateLog = null
                };

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

                author.DeleteDateLog = DateTime.Now;
                author.State = Entities.Enums.State.Silindi;
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
