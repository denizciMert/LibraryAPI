using LibraryAPI.Entities.DTOs.MemberDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
    public class MemberMapper
    {
        public ApplicationUser PostUser(MemberPost tPost)
        {
            var user = new ApplicationUser
            {
                FirstName = tPost.FirstName,
                MiddleName = tPost.MiddleName,
                LastName = tPost.LastName,
                UserName = tPost.UserName,
                IdentityNo = tPost.IdentityNo,
                DateOfBirth = tPost.DateOfBirth,
                Gender = (Gender)tPost.GenderId!,
                CountryId = tPost.CountryId,
                UserRole = (UserRole)tPost.UserRoleId!,
                DateOfRegister = DateTime.Now,
                Email = tPost.Email,
                PhoneNumber = tPost.Phone,
                Banned = false,
                UserImagePath = tPost.UserImagePath,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return user;
        }

        public Member PostMember(ApplicationUser user, MemberPost memberPost)
        {
            var member = new Member
            {
                Id = user.Id,
                EducationDegree = memberPost.EducationDegree
            };

            return member;
        }

        public ApplicationUser UpdateUser(ApplicationUser user, MemberPost tPost)
        {
            user.FirstName = tPost.FirstName;
            user.MiddleName = tPost.MiddleName;
            user.LastName = tPost.LastName;
            user.UserName = tPost.UserName;
            user.IdentityNo = tPost.IdentityNo;
            user.DateOfBirth = tPost.DateOfBirth;
            user.Gender = (Gender)tPost.GenderId!;
            user.CountryId = tPost.CountryId;
            user.UserRole = (UserRole)tPost.UserRoleId!;
            user.DateOfRegister = user.DateOfRegister;
            user.Email = tPost.Email;
            user.PhoneNumber = tPost.Phone;
            user.Banned = false;
            user.UserImagePath = tPost.UserImagePath;
            user.UpdateDateLog = DateTime.Now;
            user.DeleteDateLog = null;
            user.State = State.Güncellendi;

            return user;
        }

        public Member UpdateMember(Member member, MemberPost memberPost)
        {
            member.EducationDegree = memberPost.EducationDegree;

            return member;
        }

        public Member DeleteEntity(Member member)
        {
            member.ApplicationUser.DeleteDateLog = DateTime.Now;
            member.ApplicationUser.State = State.Silindi;

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

