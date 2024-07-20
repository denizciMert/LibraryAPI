using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.DAL;
using LibraryAPI.Entities.DTOs.MemberDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class MemberService : ILibraryUserManager
    {
        private readonly ApplicationDbContext _context;

        public MemberService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
/*
private readonly ApplicationDbContext _context;
private readonly MemberData _memberData;
private readonly MemberMapper _memberMapper;

public MemberService(ApplicationDbContext context)
{
    _context = context;
    _memberData = new MemberData(_context);
    _memberMapper = new MemberMapper();
}

public async Task<ServiceResult<IEnumerable<MemberGet>>> GetAllAsync()
{
    try
    {
        var members = await _memberData.SelectAll();
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

public async Task<ServiceResult<MemberGet>> GetByIdAsync(int id)
{
    try
    {
        var member = await _memberData.SelectForEntity(id);
        if (member == null)
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

public async Task<ServiceResult<Member>> GetWithDataByIdAsync(int id)
{
    try
    {
        var member = await _memberData.SelectForEntity(id);
        if (member == null)
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
        var newMember = _memberMapper.PostEntity(tPost);
        _memberData.AddToContext(newMember);
        await _memberData.SaveContext();
        var result = await GetByIdAsync(newMember.Id);
        return ServiceResult<MemberGet>.SuccessResult(result.Data);
    }
    catch (Exception ex)
    {
        return ServiceResult<MemberGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
    }
}

public async Task<ServiceResult<MemberGet>> UpdateAsync(int id, MemberPost tPost)
{
    try
    {
        var member = await _memberData.SelectForEntity(id);
        if (member == null)
        {
            return ServiceResult<MemberGet>.FailureResult("Üye verisi bulunmuyor.");
        }
        _memberMapper.UpdateEntity(member, tPost);
        await _memberData.SaveContext();
        var updatedMember = _memberMapper.MapToDto(member);
        return ServiceResult<MemberGet>.SuccessResult(updatedMember);
    }
    catch (Exception ex)
    {
        return ServiceResult<MemberGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
    }
}

public async Task<ServiceResult<bool>> DeleteAsync(int id)
{
    try
    {
        var member = await _memberData.SelectForEntity(id);
        if (member == null)
        {
            return ServiceResult<bool>.FailureResult("Üye verisi bulunmuyor.");
        }
        _memberData.DeleteFromContext(member);
        await _memberData.SaveContext();
        return ServiceResult<bool>.SuccessResult(true);
    }
    catch (Exception ex)
    {
        return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
    }
}
*/