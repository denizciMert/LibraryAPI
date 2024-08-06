using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.ReservationDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// ReservationService class implements the ILibraryServiceManager interface and provides
    /// functionalities related to reservation management.
    /// </summary>
    public class ReservationService : ILibraryServiceManager<ReservationGet, ReservationPost, Reservation>
    {
        // Private fields to hold instances of data and mappers.
        private readonly ReservationData _reservationData;
        private readonly ReservationMapper _reservationMapper;

        /// <summary>
        /// Constructor to initialize the ReservationService with necessary dependencies.
        /// </summary>
        public ReservationService(ApplicationDbContext context)
        {
            _reservationData = new ReservationData(context);
            _reservationMapper = new ReservationMapper();
        }

        /// <summary>
        /// Retrieves all reservations.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<ReservationGet>>> GetAllAsync()
        {
            try
            {
                var reservations = await _reservationData.SelectAllFiltered();
                if (reservations.Count == 0)
                {
                    return ServiceResult<IEnumerable<ReservationGet>>.FailureResult("Rezervasyon verisi bulunmuyor.");
                }
                List<ReservationGet> reservationGets = new List<ReservationGet>();
                foreach (var reservation in reservations)
                {
                    var reservationGet = _reservationMapper.MapToDto(reservation);
                    reservationGets.Add(reservationGet);
                }
                return ServiceResult<IEnumerable<ReservationGet>>.SuccessResult(reservationGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<ReservationGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all reservations with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<Reservation>>> GetAllWithDataAsync()
        {
            try
            {
                var reservations = await _reservationData.SelectAll();
                if (reservations.Count == 0)
                {
                    return ServiceResult<IEnumerable<Reservation>>.FailureResult("Rezervasyon verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Reservation>>.SuccessResult(reservations);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Reservation>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a reservation by its ID.
        /// </summary>
        public async Task<ServiceResult<ReservationGet>> GetByIdAsync(int id)
        {
            try
            {
                Reservation? nullReservation = null;
                var reservation = await _reservationData.SelectForEntity(id);
                if (reservation == nullReservation)
                {
                    return ServiceResult<ReservationGet>.FailureResult("Rezervasyon verisi bulunmuyor.");
                }
                var reservationGet = _reservationMapper.MapToDto(reservation);
                return ServiceResult<ReservationGet>.SuccessResult(reservationGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<ReservationGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a reservation with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<Reservation>> GetWithDataByIdAsync(int id)
        {
            try
            {
                Reservation? nullReservation = null;
                var reservation = await _reservationData.SelectForEntity(id);
                if (reservation == nullReservation)
                {
                    return ServiceResult<Reservation>.FailureResult("Rezervasyon verisi bulunmuyor.");
                }
                return ServiceResult<Reservation>.SuccessResult(reservation);
            }
            catch (Exception ex)
            {
                return ServiceResult<Reservation>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new reservation.
        /// </summary>
        public async Task<ServiceResult<ReservationGet>> AddAsync(ReservationPost tPost)
        {
            try
            {
                if (await _reservationData.IsRegistered(tPost))
                {
                    return ServiceResult<ReservationGet>.FailureResult("Bu rezervasyon zaten eklenmiş.");
                }
                var newReservation = _reservationMapper.PostEntity(tPost);
                _reservationData.AddToContext(newReservation);
                await _reservationData.SaveContext();
                var result = await GetByIdAsync(newReservation.Id);
                return ServiceResult<ReservationGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<ReservationGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing reservation.
        /// </summary>
        public async Task<ServiceResult<ReservationGet>> UpdateAsync(int id, ReservationPost tPost)
        {
            try
            {
                if (await _reservationData.IsRegistered(tPost))
                {
                    return ServiceResult<ReservationGet>.FailureResult("Bu rezervasyon zaten eklenmiş.");
                }
                Reservation? nullReservation = null;
                var reservation = await _reservationData.SelectForEntity(id);
                if (reservation == nullReservation)
                {
                    return ServiceResult<ReservationGet>.FailureResult("Rezervasyon verisi bulunmuyor.");
                }
                _reservationMapper.UpdateEntity(reservation, tPost);
                await _reservationData.SaveContext();
                var updatedReservation = _reservationMapper.MapToDto(reservation);
                return ServiceResult<ReservationGet>.SuccessResult(updatedReservation);
            }
            catch (Exception ex)
            {
                return ServiceResult<ReservationGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes a reservation by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                Reservation? nullReservation = null;
                var reservation = await _reservationData.SelectForEntity(id);
                if (reservation == nullReservation)
                {
                    return ServiceResult<bool>.FailureResult("Rezervasyon verisi bulunmuyor.");
                }
                _reservationMapper.DeleteEntity(reservation);
                await _reservationData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
