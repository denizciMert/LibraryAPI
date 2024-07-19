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

        public Category PostEntity(CategoryPost dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return category;
        }

        public Category UpdateEntity(Category category, CategoryPost categoryPost)
        {
            category.CategoryName = categoryPost.CategoryName;
            category.CreationDateLog = category.CreationDateLog;
            category.UpdateDateLog = DateTime.Now;
            category.DeleteDateLog = null;
            category.State = State.Güncellendi;

            return category;
        }

        public Category DeleteEntity(Category category)
        {
            category.DeleteDateLog = DateTime.Now;
            category.State = State.Silindi;

            return category;
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

