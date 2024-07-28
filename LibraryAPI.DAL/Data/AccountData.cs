using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class AccountData(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        ApplicationDbContext context
        ) : IAccountBase
    {
        public async Task<ApplicationUser> FindUserByUserNameAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            
            return user;
        }

        public async Task<bool> UserSignInAsync(ApplicationUser user, string password)
        {
            return signInManager.PasswordSignInAsync(user, password, false, false).Result.Succeeded;
        }

        public async Task<bool> PasswordCheck(ApplicationUser user, string password)
        {
            var result = await userManager.CheckPasswordAsync(user, password);
            return result;
        }

        public Task<bool> UserSignOutAsync()
        {
            return Task.FromResult(signInManager.SignOutAsync().IsCompletedSuccessfully);
        }

        public async Task<string> GeneratePasswordResetToken(ApplicationUser user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<string> GenerateEmailChangeToken(ApplicationUser user, string newEmail)
        {
            return await userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        public async Task<string> GenerateEmailConfirmToken(ApplicationUser user)
        {
            return await userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<bool> PasswordChange(ApplicationUser user, string newPassword, string token)
        {
            return userManager.ResetPasswordAsync(user, token, newPassword).Result.Succeeded;
        }

        public async Task<bool> EmailChange(ApplicationUser user, string newEmail, string token)
        {
            var result = userManager.ChangeEmailAsync(user, newEmail, token).Result.Succeeded;
            await userManager.UpdateNormalizedEmailAsync(user);
            return result;
        }

        public async Task<bool> EmailConfirm(ApplicationUser user, string token)
        {
            return userManager.ConfirmEmailAsync(user, token).Result.Succeeded;
        }

        public async Task<bool> IsEmailConfirmed(ApplicationUser user)
        {
            return userManager.IsEmailConfirmedAsync(user).Result;
        }

        public async Task<bool> ChangeUserRole(ApplicationUser user)
        {
            var role0 = 0;
            var role1 = 1;
            var role0Name = ((UserRole)role0).ToString();
            var role1Name = ((UserRole)role1).ToString();
            var isUserRole0 = await userManager.IsInRoleAsync(user, role0Name);
            if (!isUserRole0)
            {
                var isUserRole1 = await userManager.IsInRoleAsync(user, role1Name);
                if (!isUserRole1)
                {
                    return false;
                }

                return true;
            }
            var removeRole = userManager.RemoveFromRoleAsync(user, role0Name).Result;
            if (!removeRole.Succeeded)
            {
                return false;
            }
            var addRole = userManager.AddToRoleAsync(user, role1Name).Result;
            if (!addRole.Succeeded)
            {
                return false;
            }
            return true;
        }

        public async Task<IList<string>> GetRoles(ApplicationUser user)
        {
            var result = await userManager.GetRolesAsync(user);
            return result;
        }

        public async Task<IList<Claim>> GetClaims(ApplicationUser user)
        {
            var result = await userManager.GetClaimsAsync(user);
            return result;
        }

        public async Task<IdentityResult> UpdateUser(ApplicationUser user)
        {
            var result = await userManager.UpdateAsync(user);
            return result;
        }

        public async Task<List<Loan>> GetLoans(ApplicationUser user)
        {
            var result = await context.Loans
                .Include(x=>x.Book)
                .ThenInclude(x=>x.BookCopies)
                .Include(x=>x.Employee)
                .ThenInclude(x=>x.ApplicationUser)
                .Include(x=>x.Member)
                .ThenInclude(x=>x.ApplicationUser)
                .Where(x=>x.Member.ApplicationUser.Id == user.Id)
                .Where(x=>x.Active==true)
                .Where(x=>x.State!=State.Silindi)
                .ToListAsync();
            return result;
        }

        public async Task<List<Loan>> GetReturnedLoans(ApplicationUser user)
        {
            var result = context.Loans
                .Include(x => x.Book)
                .Include(x => x.Employee)
                .ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Member)
                .ThenInclude(x => x.ApplicationUser)
                .Where(x => x.Member.ApplicationUser.Id == user.Id)
                .Where(x => x.Active == false)
                .Where(x =>x.State!= State.Silindi)
                .ToList();
            return result;
        }

        public async Task ChangeReserveValueForReturnedLoan(int bookId,int copyNo)
        {
            var bookCopies = await context.BookCopies.Include(x => x.Book).Where(x => x.BookId == bookId).ToListAsync();
            foreach(var copy in bookCopies)
            {
                if (copy.CopyNo==copyNo)
                {
                    copy.Reserved = false;
                    await context.SaveChangesAsync();
                    break;
                }
            }
        }

        public async Task<List<Penalty>> GetPenalties(ApplicationUser user)
        {
            var result = context.Penalties
                .Include(x => x.Loan)
                .ThenInclude(x=>x.Book)
                .Include(x => x.Loan)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x=>x.ApplicationUser)
                .Include(x => x.Member)
                .ThenInclude(x => x.ApplicationUser)
                .Include(x => x.PenaltyType)
                .Where(x => x.Member.ApplicationUser.Id == user.Id)
                .Where(x => x.Active == true)
                .ToList();
            return result;
        }

        public async Task AddRatingAndSave(BookRating rating)
        {
            context.BookRatings.Add(rating);
            await context.SaveChangesAsync();
        }

        public async Task CalculateRating(int bookId)
        {
            var rates = await context.BookRatings
                .Include(x => x.Book)
                .Include(x => x.Member)
                .ThenInclude(x => x.ApplicationUser)
                .Where(x => x.Book.Id == bookId)
                .Select(x=>x.Rate)
                .ToListAsync();

            var ratesSum = rates.Sum();
            var voteCount = rates.Count;

            var rating = ratesSum / voteCount;

            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == bookId);

            book.Rating = rating;
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsRatedBefore(ApplicationUser user, int bookId, float rating)
        {
            var data = await context.BookRatings.FirstOrDefaultAsync(x =>
                x.RatedBookId == bookId && x.RaterMemberId == user.Id);

            if (data!=null)
            {
                data.Rate = rating;
                await context.SaveChangesAsync();
                await CalculateRating(bookId);
                return true;
            }
            return false;
        }

        public async Task CheckUserStatus(ApplicationUser user)
        {
            if (user.Banned==false)
            {
                var loans = await context.Loans
                    .Where(x => x.LoanedMemberId == user.Id)
                    .Where(x => x.Active == true)
                    .Where(x => x.State != State.Silindi)
                    .ToListAsync();

                foreach (var loan in loans)
                {
                    if (loan.DueDate < DateTime.Now)
                    {
                        user.Banned = true;
                        var penalty = new Penalty
                        {
                            Active = true,
                            CreationDateLog = DateTime.Now,
                            LoanId = loan.Id,
                            PenaltiedMembeId = user.Id,
                            PenaltyTypeId = 1,
                            State = State.Eklendi,
                            DeleteDateLog = null,
                            UpdateDateLog = null
                        };
                        context.Penalties.Add(penalty);
                        
                    }
                }
                await userManager.UpdateAsync(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> SaveChanges()
        {
            var result = context.SaveChangesAsync().IsCompletedSuccessfully;
            return result;
        }
    }
}