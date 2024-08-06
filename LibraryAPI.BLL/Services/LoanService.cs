using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.LoanDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// LoanService class implements the ILibraryServiceManager interface and provides
    /// functionalities related to loan management.
    /// </summary>
    public class LoanService : ILibraryServiceManager<LoanGet, LoanPost, Loan>
    {
        // Private fields to hold instances of data and mappers.
        private readonly LoanData _loanData;
        private readonly LoanMapper _loanMapper;

        /// <summary>
        /// Constructor to initialize the LoanService with necessary dependencies.
        /// </summary>
        public LoanService(ApplicationDbContext context)
        {
            _loanData = new LoanData(context);
            _loanMapper = new LoanMapper();
        }

        /// <summary>
        /// Retrieves all loans.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<LoanGet>>> GetAllAsync()
        {
            try
            {
                var loans = await _loanData.SelectAllFiltered();
                if (loans.Count == 0)
                {
                    return ServiceResult<IEnumerable<LoanGet>>.FailureResult("Ödünç alma verisi bulunmuyor.");
                }
                List<LoanGet> loanGets = new List<LoanGet>();
                foreach (var loan in loans)
                {
                    var loanGet = _loanMapper.MapToDto(loan);
                    loanGets.Add(loanGet);
                }
                return ServiceResult<IEnumerable<LoanGet>>.SuccessResult(loanGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<LoanGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all loans with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<Loan>>> GetAllWithDataAsync()
        {
            try
            {
                var loans = await _loanData.SelectAll();
                if (loans.Count == 0)
                {
                    return ServiceResult<IEnumerable<Loan>>.FailureResult("Ödünç alma verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Loan>>.SuccessResult(loans);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Loan>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a loan by its ID.
        /// </summary>
        public async Task<ServiceResult<LoanGet>> GetByIdAsync(int id)
        {
            try
            {
                Loan? nullLoan = null;
                var loan = await _loanData.SelectForLoan(id);
                if (loan == nullLoan)
                {
                    return ServiceResult<LoanGet>.FailureResult("Ödünç alma verisi bulunmuyor.");
                }
                var loanGet = _loanMapper.MapToDto(loan);
                return ServiceResult<LoanGet>.SuccessResult(loanGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<LoanGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a loan with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<Loan>> GetWithDataByIdAsync(int id)
        {
            try
            {
                Loan? nullLoan = null;
                var loan = await _loanData.SelectForEntity(id);
                if (loan == nullLoan)
                {
                    return ServiceResult<Loan>.FailureResult("Ödünç alma verisi bulunmuyor.");
                }
                return ServiceResult<Loan>.SuccessResult(loan);
            }
            catch (Exception ex)
            {
                return ServiceResult<Loan>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new loan.
        /// </summary>
        public async Task<ServiceResult<LoanGet>> AddAsync(LoanPost tPost)
        {
            try
            {
                if (await _loanData.IsRegistered(tPost))
                {
                    return ServiceResult<LoanGet>.FailureResult("Bu kayıt zaten eklenmiş.");
                }
                var copyResult = await _loanData.CanTakeLoanAndCalculateStorage(tPost.BookId, tPost.CopyNo);
                if (!copyResult)
                {
                    return ServiceResult<LoanGet>.FailureResult("Girilen kopya numarası uygun değil");
                }
                var newLoan = _loanMapper.PostEntity(tPost);
                _loanData.AddToContext(newLoan);
                await _loanData.SaveContext();
                var result = await GetByIdAsync(newLoan.Id);
                return ServiceResult<LoanGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<LoanGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing loan.
        /// </summary>
        public async Task<ServiceResult<LoanGet>> UpdateAsync(int id, LoanPost tPost)
        {
            try
            {
                Loan? nullLoan = null;
                var loan = await _loanData.SelectForEntity(id);
                if (loan == nullLoan)
                {
                    return ServiceResult<LoanGet>.FailureResult("Ödünç alma verisi bulunmuyor.");
                }
                if (tPost.CopyNo != loan.CopyNo)
                {
                    await _loanData.ChangeReserveBook(loan.BookId, loan.CopyNo);
                    var copyResult = await _loanData.CanTakeLoanAndCalculateStorage(tPost.BookId, tPost.CopyNo);
                    if (!copyResult)
                    {
                        return ServiceResult<LoanGet>.FailureResult("Girilen kopya numarası uygun değil");
                    }
                }
                _loanMapper.UpdateEntity(loan, tPost);
                await _loanData.SaveContext();
                var updatedLoan = _loanMapper.MapToDto(loan);
                return ServiceResult<LoanGet>.SuccessResult(updatedLoan);
            }
            catch (Exception ex)
            {
                return ServiceResult<LoanGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes a loan by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                Loan? nullLoan = null;
                var loan = await _loanData.SelectForLoan(id);
                if (loan == nullLoan)
                {
                    return ServiceResult<bool>.FailureResult("Ödünç alma verisi bulunmuyor.");
                }
                _loanMapper.DeleteEntity(loan);
                await _loanData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
