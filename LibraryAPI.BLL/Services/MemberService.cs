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
    public class MemberService : ILibraryUserManager<MemberGet, MemberPost, Member>
    {
        private readonly MemberData _memberData;
        private readonly MemberMapper _memberMapper;

        public MemberService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _memberData = new MemberData(context, userManager);
            _memberMapper = new MemberMapper();
        }

        public async Task<ServiceResult<IEnumerable<MemberGet>>> GetAllAsync()
        {
            try
            {
                var members = await _memberData.SelectAllFiltered();
                if (members == null || members.Count == 0)
                {
                    return ServiceResult<IEnumerable<MemberGet>>.FailureResult("Üye verisi bulunmuyor.");
                }

                List<MemberGet> memberGets = new List<MemberGet>();
                foreach (var member in members)
                {
                    var memberGet = _memberMapper.MapToDto(member);
                    memberGets.Add(memberGet);
                }

                return ServiceResult<IEnumerable<MemberGet>>.SuccessResult(memberGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<MemberGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<Member>>> GetAllWithDataAsync()
        {
            try
            {
                var members = await _memberData.SelectAll();
                if (members == null || members.Count == 0)
                {
                    return ServiceResult<IEnumerable<Member>>.FailureResult("Üye verisi bulunmuyor.");
                }

                return ServiceResult<IEnumerable<Member>>.SuccessResult(members);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Member>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<MemberGet>> GetByIdAsync(string id)
        {
            try
            {
                var member = await _memberData.SelectForUser(id);
                if (member.ApplicationUser == null)
                {
                    return ServiceResult<MemberGet>.FailureResult("Üye verisi bulunmuyor.");
                }

                var memberGet = _memberMapper.MapToDto(member);
                return ServiceResult<MemberGet>.SuccessResult(memberGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<MemberGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

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
                return ServiceResult<Member>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<MemberGet>> AddAsync(MemberPost tPost)
        {
            try
            {
                if (await _memberData.IsRegistered(tPost))
                {
                    return ServiceResult<MemberGet>.FailureResult("Bu üye zaten eklenmiş.");
                }

                var newUser = _memberMapper.PostUser(tPost);
                await _memberData.SaveUser(newUser, tPost.Password);
                var newMember = _memberMapper.PostMember(newUser, tPost);
                _memberData.AddToContext(newMember);
                await _memberData.SaveContext();
                await _memberData.AddRoleToUser(newMember.ApplicationUser, ((UserRole)tPost.UserRoleId).ToString());
                var result = await GetByIdAsync(newMember.Id);
                return ServiceResult<MemberGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<MemberGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<MemberGet>> UpdateAsync(string id, MemberPost tPost)
        {
            try
            {
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
                return ServiceResult<MemberGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

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
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}