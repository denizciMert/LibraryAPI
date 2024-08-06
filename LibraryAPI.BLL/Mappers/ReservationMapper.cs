using LibraryAPI.Entities.DTOs.ReservationDTO; // Importing the DTOs for Reservation
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Reservation entities and DTOs
    public class ReservationMapper
    {
        // Method to map ReservationPost DTO to Reservation entity
        public Reservation MapToEntity(ReservationPost reservationPost)
        {
            var entity = new Reservation
            {
                MemberId = reservationPost.MemberId,
                EmployeeId = reservationPost.EmployeeId,
                TableId = reservationPost.TableId,
                ReservationStart = reservationPost.ReservationStart,
                ReservationEnd = reservationPost.ReservationStart.AddHours(4)
            };

            return entity;
        }

        // Method to map ReservationPost DTO to Reservation entity with additional fields
        public Reservation PostEntity(ReservationPost reservationPost)
        {
            var reservation = new Reservation
            {
                MemberId = reservationPost.MemberId,
                EmployeeId = reservationPost.EmployeeId,
                TableId = reservationPost.TableId,
                ReservationStart = reservationPost.ReservationStart,
                ReservationEnd = reservationPost.ReservationStart.AddHours(4),
                Active = true,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return reservation;
        }

        // Method to update an existing Reservation entity with ReservationPost DTO data
        public Reservation UpdateEntity(Reservation reservation, ReservationPost reservationPost)
        {
            reservation.MemberId = reservationPost.MemberId;
            reservation.EmployeeId = reservationPost.EmployeeId;
            reservation.TableId = reservationPost.TableId;
            reservation.ReservationStart = reservationPost.ReservationStart;
            reservation.ReservationEnd = reservationPost.ReservationStart.AddHours(4);
            reservation.Active = reservation.Active;
            reservation.CreationDateLog = reservation.CreationDateLog;
            reservation.UpdateDateLog = DateTime.Now;
            reservation.DeleteDateLog = null;
            reservation.State = State.Güncellendi;

            return reservation;
        }

        // Method to mark a Reservation entity as deleted
        public Reservation DeleteEntity(Reservation reservation)
        {
            reservation.DeleteDateLog = DateTime.Now;
            reservation.State = State.Silindi;
            reservation.Active = false;

            return reservation;
        }

        // Method to map Reservation entity to ReservationGet DTO
        public ReservationGet MapToDto(Reservation reservation)
        {
            var dto = new ReservationGet
            {
                Id = reservation.Id,
                MemberName = reservation.Member?.ApplicationUser?.UserName,
                UserName = reservation.Member?.ApplicationUser?.UserName,
                EmployeeName = reservation.Employee?.ApplicationUser?.FirstName,
                StudyTable = reservation.StudyTable?.TableCode,
                ReservationStart = reservation.ReservationStart,
                ReservationEnd = reservation.ReservationEnd,
                Active = reservation.Active,
                CreatinDateLog = reservation.CreationDateLog,
                UpdateDateLog = reservation.UpdateDateLog,
                DeleteDateLog = reservation.DeleteDateLog,
                State = reservation.State.ToString()
            };

            return dto;
        }
    }
}
