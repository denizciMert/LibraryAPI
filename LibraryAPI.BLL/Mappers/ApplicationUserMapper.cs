using LibraryAPI.Entities.DTOs; // Importing the DTOs
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for ApplicationUser to apply partial updates
    public class ApplicationUserMapper
    {
        // Method to apply a patch DTO to an ApplicationUser entity
        public void ApplyPatch(ApplicationUser user, ApplicationUserPatch patchDto)
        {
            if (!string.IsNullOrEmpty(patchDto.FirstName))
            {
                user.FirstName = patchDto.FirstName; // Updating first name if provided
            }

            if (!string.IsNullOrEmpty(patchDto.MiddleName))
            {
                user.MiddleName = patchDto.MiddleName; // Updating middle name if provided
            }

            if (!string.IsNullOrEmpty(patchDto.LastName))
            {
                user.LastName = patchDto.LastName; // Updating last name if provided
            }

            if (!string.IsNullOrEmpty(patchDto.IdentityNo))
            {
                user.IdentityNo = patchDto.IdentityNo; // Updating identity number if provided
            }

            if (patchDto.DateOfBirth.HasValue)
            {
                user.DateOfBirth = patchDto.DateOfBirth.Value; // Updating date of birth if provided
            }

            if (patchDto.CountryId.HasValue)
            {
                user.CountryId = patchDto.CountryId.Value; // Updating country ID if provided
            }

            if (patchDto.Gender.HasValue)
            {
                user.Gender = patchDto.Gender.Value; // Updating gender if provided
            }

            user.UpdateDateLog = DateTime.Now; // Updating the update date log to current time
        }
    }
}