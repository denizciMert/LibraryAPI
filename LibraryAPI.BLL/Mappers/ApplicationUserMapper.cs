using LibraryAPI.Entities.DTOs;
using LibraryAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.BLL.Mappers
{
    public class ApplicationUserMapper
    {
        public void ApplyPatch(ApplicationUser user, ApplicationUserPatch patchDto)
        {
            if (!string.IsNullOrEmpty(patchDto.FirstName))
            {
                user.FirstName = patchDto.FirstName;
            }

            if (!string.IsNullOrEmpty(patchDto.MiddleName))
            {
                user.MiddleName = patchDto.MiddleName;
            }

            if (!string.IsNullOrEmpty(patchDto.LastName))
            {
                user.LastName = patchDto.LastName;
            }

            if (!string.IsNullOrEmpty(patchDto.IdentityNo))
            {
                user.IdentityNo = patchDto.IdentityNo;
            }

            if (patchDto.DateOfBirth.HasValue)
            {
                user.DateOfBirth = patchDto.DateOfBirth.Value;
            }

            if (patchDto.CountryId.HasValue)
            {
                user.CountryId = patchDto.CountryId.Value;
            }

            if (patchDto.Gender.HasValue)
            {
                user.Gender = patchDto.Gender.Value;
            }

            user.UpdateDateLog = DateTime.Now;
        }
    }
}
