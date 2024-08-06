using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.MemberDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// MemberService class implements the ILibraryUserManager interface and provides
    /// functionalities related to member management.
    /// </summary>
    public class MemberService : ILibraryUserManager<MemberGet, MemberPost, Member>
    {
        // Private fields to hold instances of data and mappers.
        private readonly MemberData _memberData;
        private readonly MemberMapper _memberMapper;

        /// <summary>
        /// Constructor to initialize the MemberService with necessary dependencies.
        /// </summary>
        public MemberService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _memberData = new MemberData(context, userManager);
            _memberMapper = new MemberMapper();
        }

        /// <summary>
        /// Retrieves all members.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<MemberGet>>> GetAllAsync()
        {
            try
            {
                var members = await _memberData.SelectAllFiltered();
                if (members.Count == 0)
                {
                    return ServiceResult<IEnumerable<MemberGet>>.FailureResult("Üye verisi bulunmuyor.");
                }

                List<MemberGet> memberGets = new List<MemberGet>();
                foreach (var member in members)
                {
                    var addresses = await _memberData.GetUserAddresses(member.Id);
                    var loans = await _memberData.GetUserLoans(member.Id);
                    var penalties = await _memberData.GetUserPenalties(member.Id);
                    var memberGet = _memberMapper.MapToDto(member);
                    memberGet.Adresses = addresses;
                    memberGet.Loans = loans;
                    memberGet.Penalties = penalties;
                    memberGets.Add(memberGet);
                }

                return ServiceResult<IEnumerable<MemberGet>>.SuccessResult(memberGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<MemberGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all members with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<Member>>> GetAllWithDataAsync()
        {
            try
            {
                var members = await _memberData.SelectAll();
                if (members.Count == 0)
                {
                    return ServiceResult<IEnumerable<Member>>.FailureResult("Üye verisi bulunmuyor.");
                }

                return ServiceResult<IEnumerable<Member>>.SuccessResult(members);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Member>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a member by its ID.
        /// </summary>
        public async Task<ServiceResult<MemberGet>> GetByIdAsync(string id)
        {
            try
            {
                var member = await _memberData.SelectForUser(id);
                if (member.ApplicationUser == null)
                {
                    return ServiceResult<MemberGet>.FailureResult("Üye verisi bulunmuyor.");
                }

                var addresses = await _memberData.GetUserAddresses(member.Id);
                var loans = await _memberData.GetUserLoans(member.Id);
                var penalties = await _memberData.GetUserPenalties(member.Id);
                var memberGet = _memberMapper.MapToDto(member);
                memberGet.Adresses = addresses;
                memberGet.Loans = loans;
                memberGet.Penalties = penalties;

                return ServiceResult<MemberGet>.SuccessResult(memberGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<MemberGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a member with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<Member>> GetWithDataByIdAsync(string id)
        {
            try
            {
                var member = await _memberData.SelectForUser(id);
                if (member.ApplicationUser == null)
                {
                    return ServiceResult<Member>.FailureResult("Üye verisi bulunmuyor.");
                }

                return ServiceResult<Member>.SuccessResult(member);
            }
            catch (Exception ex)
            {
                return ServiceResult<Member>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new member.
        /// </summary>
        public async Task<ServiceResult<MemberGet>> AddAsync(MemberPost tPost)
        {
            try
            {
                if (tPost.DateOfBirth > DateTime.Now ||
                    tPost.DateOfBirth.Year > (DateTime.Now.Year - 18) ||
                    DateTime.Now.Year - tPost.DateOfBirth.Year > 123)
                {
                    return ServiceResult<MemberGet>.FailureResult("Üye 18 yaşından küçük ve 123 yaşından büyük olamaz.");
                }
                if (await _memberData.IsRegistered(tPost))
                {
                    return ServiceResult<MemberGet>.FailureResult("Bu üye zaten eklenmiş.");
                }

                tPost.UserRoleId = 0;
                var newUser = _memberMapper.PostUser(tPost);
                await _memberData.SaveUser(newUser, tPost.Password);
                var newMember = _memberMapper.PostMember(newUser, tPost);
                _memberData.AddToContext(newMember);
                await _memberData.SaveContext();
                await _memberData.AddRoleToUser(newMember.ApplicationUser!, ((UserRole)tPost.UserRoleId).ToString());
                var result = await GetByIdAsync(newMember.Id);
                return ServiceResult<MemberGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<MemberGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing member.
        /// </summary>
        public async Task<ServiceResult<MemberGet>> UpdateAsync(string id, MemberPost tPost)
        {
            try
            {
                if (tPost.DateOfBirth > DateTime.Now ||
                    tPost.DateOfBirth.Year > (DateTime.Now.Year - 18) ||
                    DateTime.Now.Year - tPost.DateOfBirth.Year > 123)
                {
                    return ServiceResult<MemberGet>.FailureResult("Üye 18 yaşından küçük ve 123 yaşından büyük olamaz.");
                }
                if (await _memberData.IsRegistered(tPost))
                {
                    return ServiceResult<MemberGet>.FailureResult("Bu üye zaten eklenmiş.");
                }

                var member = await _memberData.SelectForUser(id);
                if (member.ApplicationUser == null)
                {
                    return ServiceResult<MemberGet>.FailureResult("Üye verisi bulunmuyor.");
                }

                _memberMapper.UpdateUser(member.ApplicationUser, tPost);
                await _memberData.UpdateUser(member.ApplicationUser);
                _memberMapper.UpdateMember(member, tPost);
                await _memberData.SaveContext();
                var updatedMember = _memberMapper.MapToDto(member);
                return ServiceResult<MemberGet>.SuccessResult(updatedMember);
            }
            catch (Exception ex)
            {
                return ServiceResult<MemberGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes a member by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(string id)
        {
            try
            {
                var member = await _memberData.SelectForUser(id);
                if (member.ApplicationUser == null)
                {
                    return ServiceResult<bool>.FailureResult("Üye verisi bulunmuyor.");
                }

                _memberMapper.DeleteEntity(member);
                await _memberData.DeleteUser(member.ApplicationUser);
                await _memberData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
