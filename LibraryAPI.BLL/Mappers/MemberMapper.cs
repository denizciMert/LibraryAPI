using System;
using LibraryAPI.Entities.DTOs.MemberDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
    public class MemberMapper
    {
        public Member MapToEntity(MemberPost dto)
        {
            var member = new Member
            {
                Id = Guid.NewGuid().ToString(),
                ApplicationUser = new ApplicationUser
                {
                    UserName = dto.UserName,
                    IdentityNo = dto.IdentityNo,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = (Gender)dto.GenderId,
                    CountryId = dto.CountryId,
                    UserRole = (UserRole)dto.UserRoleId
                }
            };
            return member;
        }

        public MemberGet MapToDto(Member entity)
        {
            var dto = new MemberGet
            {
                Id = entity.Id,
                MemberName = $"{entity.ApplicationUser?.FirstName} {entity.ApplicationUser?.LastName}",
                IdentityNo = entity.ApplicationUser?.IdentityNo,
                UserName = entity.ApplicationUser?.UserName,
                Email = entity.ApplicationUser?.Email,
                Phone = entity.ApplicationUser?.PhoneNumber,
                DateOfBirth = entity.ApplicationUser?.DateOfBirth,
                Gender = entity.ApplicationUser?.Gender.ToString(),
                Country = entity.ApplicationUser?.Country?.CountryName,
                EducationDegree = entity.EducationDegree,
                UserRole = entity.ApplicationUser?.UserRole.ToString(),
                DateOfRegister = entity.ApplicationUser?.DateOfRegister,
                UpdateDateLog = entity.ApplicationUser.UpdateDateLog,
                DeleteDateLog = entity.ApplicationUser.DeleteDateLog,
                State = entity.ApplicationUser.State.ToString(),
                Banned = entity.ApplicationUser.Banned,
                Adresses = entity.ApplicationUser?.Addresses?.Select(a => a.AddressString+" "+a.District.DistrictName+" "+a.District.City.CityName+" "+a.District.City.Country.CountryName).ToList(),
                Loans = entity.Loans?.Select(l => l.Id.ToString()).ToList(),
                Penalties = entity.Penalties?.Select(p => p.Id.ToString()).ToList(),
                ImagePath = entity.ApplicationUser?.UserImagePath
            };
            return dto;
        }
    }
}

