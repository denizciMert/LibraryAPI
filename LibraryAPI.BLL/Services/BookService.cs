using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.BookDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// BookService class implements the ILibraryServiceManager interface and provides
    /// functionalities related to book management.
    /// </summary>
    public class BookService : ILibraryServiceManager<BookGet, BookPost, Book>
    {
        // Private fields to hold instances of data and mappers.
        private readonly BookData _bookData;
        private readonly BookMapper _bookMapper;

        /// <summary>
        /// Constructor to initialize the BookService with necessary dependencies.
        /// </summary>
        public BookService(ApplicationDbContext context)
        {
            _bookData = new BookData(context);
            _bookMapper = new BookMapper();
        }

        /// <summary>
        /// Retrieves all books.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<BookGet>>> GetAllAsync()
        {
            try
            {
                var books = await _bookData.SelectAllFiltered();
                if (books.Count == 0)
                {
                    return ServiceResult<IEnumerable<BookGet>>.FailureResult("Kitap verisi bulunmuyor.");
                }
                List<BookGet> bookGets = new List<BookGet>();
                foreach (var book in books)
                {
                    var bookGet = _bookMapper.MapToDto(book);
                    bookGets.Add(bookGet);
                }
                return ServiceResult<IEnumerable<BookGet>>.SuccessResult(bookGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<BookGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all books with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<Book>>> GetAllWithDataAsync()
        {
            try
            {
                var books = await _bookData.SelectAll();
                if (books.Count == 0)
                {
                    return ServiceResult<IEnumerable<Book>>.FailureResult("Kitap verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Book>>.SuccessResult(books);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Book>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a book by its ID.
        /// </summary>
        public async Task<ServiceResult<BookGet>> GetByIdAsync(int id)
        {
            try
            {
                Book? nullBook = null;
                var book = await _bookData.SelectForEntity(id);
                if (book == nullBook)
                {
                    return ServiceResult<BookGet>.FailureResult("Kitap verisi bulunmuyor.");
                }
                var bookGet = _bookMapper.MapToDto(book);
                return ServiceResult<BookGet>.SuccessResult(bookGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<BookGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a book with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<Book>> GetWithDataByIdAsync(int id)
        {
            try
            {
                Book? nullBook = null;
                var book = await _bookData.SelectForEntity(id);
                if (book == nullBook)
                {
                    return ServiceResult<Book>.FailureResult("Kitap verisi bulunmuyor.");
                }
                return ServiceResult<Book>.SuccessResult(book);
            }
            catch (Exception ex)
            {
                return ServiceResult<Book>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new book.
        /// </summary>
        public async Task<ServiceResult<BookGet>> AddAsync(BookPost tPost)
        {
            try
            {
                if (await _bookData.IsRegistered(tPost))
                {
                    return ServiceResult<BookGet>.FailureResult("Bu kitap zaten eklenmiş.");
                }
                var newBook = _bookMapper.PostEntity(tPost);
                _bookData.AddToContext(newBook);
                await _bookData.SaveContext();
                var result = await GetByIdAsync(newBook.Id);
                return ServiceResult<BookGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<BookGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        public async Task<ServiceResult<BookGet>> UpdateAsync(int id, BookPost tPost)
        {
            try
            {
                Book? nullBook = null;
                var book = await _bookData.SelectForEntity(id);
                if (book == nullBook)
                {
                    return ServiceResult<BookGet>.FailureResult("Kitap verisi bulunmuyor.");
                }

                if (book.Isbn!=tPost.Isbn)
                {
                    if (await _bookData.IsRegistered(tPost))
                    {
                        return ServiceResult<BookGet>.FailureResult("Bu kitap zaten eklenmiş.");
                    }
                }
                
                _bookMapper.UpdateEntity(book, tPost);
                await _bookData.SaveContext();
                var result = _bookMapper.MapToDto(book);
                return ServiceResult<BookGet>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ServiceResult<BookGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes a book by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                Book? nullBook = null;
                var book = await _bookData.SelectForEntity(id);
                if (book == nullBook)
                {
                    return ServiceResult<bool>.FailureResult("Kitap verisi bulunmuyor.");
                }
                _bookMapper.DeleteEntity(book);
                await _bookData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
