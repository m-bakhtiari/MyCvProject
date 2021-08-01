using Microsoft.EntityFrameworkCore;
using MyCvProject.Core.Generator;
using MyCvProject.Core.ViewModels;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.Entities.Wallet;
using MyCvProject.Domain.Interfaces;
using MyCvProject.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCvProject.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyCvProjectContext _context;

        public UserRepository(MyCvProjectContext context)
        {
            _context = context;
        }


        public async Task<bool> IsExistUserName(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }

        public async Task<bool> IsExistEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<int> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<User> LoginUser(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<List<UserRole>> GetUserRoleByUserId(int userId)
        {
            return await _context.UserRoles.Include(x => x.Role).Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<User> GetUserByActiveCode(string activeCode)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.ActiveCode == activeCode);
        }

        public async Task<User> GetUserByUserName(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task UpdateUser(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ActiveAccount(string activeCode)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetUserIdByUserName(string userName)
        {
            var user = await _context.Users.SingleAsync(u => u.UserName == userName);
            return user.UserId;
        }

        public async Task<SideBarUserPanelViewModel> GetSideBarUserPanelData(string username)
        {
            return await _context.Users.Where(u => u.UserName == username).Select(u => new SideBarUserPanelViewModel()
            {
                UserName = u.UserName,
                ImageName = u.UserAvatar,
                RegisterDate = u.RegisterDate
            }).SingleAsync();
        }

        public async Task<EditProfileViewModel> GetDataForEditProfileUser(string username)
        {
            return await _context.Users.Where(u => u.UserName == username).Select(u => new EditProfileViewModel()
            {
                AvatarName = u.UserAvatar,
                Email = u.Email,
                UserName = u.UserName

            }).SingleAsync();
        }

        public async Task<bool> CompareOldPassword(string oldPassword, string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username && u.Password == oldPassword);
        }

        public async Task<int> BalanceUserWallet(string userName)
        {

            int userId = await GetUserIdByUserName(userName);

            var enter = await _context.Wallets
                .Where(w => w.UserId == userId && w.TypeId == 1 && w.IsPay)
                .Select(w => w.Amount).ToListAsync();

            var exit = await _context.Wallets
                .Where(w => w.UserId == userId && w.TypeId == 2)
                .Select(w => w.Amount).ToListAsync();

            return (enter.Sum() - exit.Sum());
        }

        public async Task<List<WalletViewModel>> GetWalletUser(string userName)
        {
            int userId = await GetUserIdByUserName(userName);

            return await _context.Wallets
                .Where(w => w.IsPay && w.UserId == userId)
                .Select(w => new WalletViewModel()
                {
                    Amount = w.Amount,
                    DateTime = w.CreateDate,
                    Description = w.Description,
                    Type = w.TypeId
                })
                .ToListAsync();
        }

        public async Task<int> AddWallet(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
            return wallet.WalletId;
        }

        public async Task<Wallet> GetWalletByWalletId(int walletId)
        {
            return await _context.Wallets.FindAsync(walletId);
        }

        public async Task UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task<UserForAdminViewModel> GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users;

            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }

            // Show Item In Page
            var take = 20;
            var skip = (pageId - 1) * take;

            var list = new UserForAdminViewModel
            {
                CurrentPage = pageId,
                PageCount = await result.CountAsync() / take,
                Users = await result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToListAsync()
            };

            return list;
        }

        public async Task<UserForAdminViewModel> GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users.IgnoreQueryFilters().Where(u => u.IsDelete);

            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }

            // Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;


            UserForAdminViewModel list = new UserForAdminViewModel
            {
                CurrentPage = pageId,
                PageCount = await result.CountAsync() / take,
                Users = await result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToListAsync()
            };

            return list;
        }

        public async Task<EditUserViewModel> GetUserForShowInEditMode(int userId)
        {
            return await _context.Users.Include(x => x.UserRoles).Where(u => u.UserId == userId)
                .Select(u => new EditUserViewModel()
                {
                    UserId = u.UserId,
                    AvatarName = u.UserAvatar,
                    Email = u.Email,
                    UserName = u.UserName,
                    UserRoles = u.UserRoles.Select(r => r.RoleId).ToList()
                }).SingleAsync();
        }

        public async Task EditUserFromAdmin(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> UserCount()
        {
            return await _context.Users.CountAsync();
        }
    }

}
