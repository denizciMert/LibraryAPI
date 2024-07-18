using System;
using LibraryAPI.Entities.DTOs.ReservationDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class ReservationMapper
	{
        public Reservation MapToEntity(ReservationPost reservationPost)
        {
            var entity = new Reservation
            {
                MemberId = reservationPost.MemberId,
                TableId = reservationPost.TableId,
                ReservationStart = reservationPost.ReservationStart,
                ReservationEnd = reservationPost.ReservationStart.AddHours(4)
            };

            return entity;
        }

        public ReservationGet MapToDto(Reservation reservation)
        {
            var dto = new ReservationGet
            {
                Id = reservation.Id,
                MemberName = reservation.Member?.ApplicationUser?.UserName,
                UserName = reservation.Member?.ApplicationUser?.UserName,
                StudyTable = reservation.StudyTable?.TableCode,
                ReservationStart = reservation.ReservationStart,
                ReservationEnd = reservation.ReservationEnd,
                CreatinDateLog = reservation.CreationDateLog,
                UpdateDateLog = reservation.UpdateDateLog,
                DeleteDateLog = reservation.DeleteDateLog,
                State = reservation.State.ToString()
            };

            return dto;
        }
    }
}

