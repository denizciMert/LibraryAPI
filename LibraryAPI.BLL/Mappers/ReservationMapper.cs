using System;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.DTOs.ReservationDTO;
using LibraryAPI.Entities.Enums;
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

        public Reservation PostEntity(ReservationPost reservationPost)
        {
            var reservation = new Reservation
            {
                MemberId = reservationPost.MemberId,
                TableId = reservationPost.TableId,
                ReservationStart = reservationPost.ReservationStart,
                ReservationEnd = reservationPost.ReservationStart.AddHours(4),
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return reservation;
        }

        public Reservation UpdateEntity(Reservation reservation, ReservationPost reservationPost)
        {
            reservation.MemberId = reservationPost.MemberId;
            reservation.TableId = reservationPost.TableId;
            reservation.ReservationStart = reservationPost.ReservationStart;
            reservation.ReservationEnd = reservationPost.ReservationStart.AddHours(4);
            reservation.CreationDateLog = reservation.CreationDateLog;
            reservation.UpdateDateLog = DateTime.Now;
            reservation.DeleteDateLog = null;
            reservation.State = State.Güncellendi;

            return reservation;
        }

        public Reservation DeleteEntity(Reservation reservation)
        {
            reservation.DeleteDateLog = DateTime.Now;
            reservation.State = State.Silindi;

            return reservation;
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

