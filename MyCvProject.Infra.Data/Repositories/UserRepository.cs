using Microsoft.EntityFrameworkCore;
using MyCvProject.Core.Generator;
using MyCvProject.Core.Security;
using MyCvProject.Core.ViewModels;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.Entities.Wallet;
using MyCvProject.Domain.Interfaces;
using MyCvProject.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyCvProject.Infra.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly MyCvProjectContext _context;

        public UserRepository(MyCvProjectContext context)
        {
            _context = context;
        }


        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public User LoginUser(string email,string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == username);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public bool ActiveAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            _context.SaveChanges();

            return true;
        }

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName).UserId;
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
        {
            return _context.Users.Where(u => u.UserName == username).Select(u => new SideBarUserPanelViewModel()
            {
                UserName = u.UserName,
                ImageName = u.UserAvatar,
                RegisterDate = u.RegisterDate
            }).Single();
        }

        public EditProfileViewModel GetDataForEditProfileUser(string username)
        {
            return _context.Users.Where(u => u.UserName == username).Select(u => new EditProfileViewModel()
            {
                AvatarName = u.UserAvatar,
                Email = u.Email,
                UserName = u.UserName

            }).Single();
        }

        public bool CompareOldPassword(string oldPassword, string username)
        {
            return _context.Users.Any(u => u.UserName == username && u.Password == oldPassword);
        }

        public int BalanceUserWallet(string userName)
        {

            int userId = GetUserIdByUserName(userName);

            var enter = _context.Wallets
                .Where(w => w.UserId == userId && w.TypeId == 1 && w.IsPay)
                .Select(w => w.Amount).ToList();

            var exit = _context.Wallets
                .Where(w => w.UserId == userId && w.TypeId == 2)
                .Select(w => w.Amount).ToList();

            return (enter.Sum() - exit.Sum());
        }

        public List<WalletViewModel> GetWalletUser(string userName)
        {
            int userId = GetUserIdByUserName(userName);

            return _context.Wallets
                .Where(w => w.IsPay && w.UserId == userId)
                .Select(w => new WalletViewModel()
                {
                    Amount = w.Amount,
                    DateTime = w.CreateDate,
                    Description = w.Description,
                    Type = w.TypeId
                })
                .ToList();
        }

        public int AddWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            _context.SaveChanges();
            return wallet.WalletId;
        }

        public Wallet GetWalletByWalletId(int walletId)
        {
            return _context.Wallets.Find(walletId);
        }

        public void UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            _context.SaveChanges();
        }

        public UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
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
            int take = 20;
            int skip = (pageId - 1) * take;


            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
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


            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public EditUserViewModel GetUserForShowInEditMode(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId)
                .Select(u => new EditUserViewModel()
                {
                    UserId = u.UserId,
                    AvatarName = u.UserAvatar,
                    Email = u.Email,
                    UserName = u.UserName,
                    UserRoles = u.UserRoles.Select(r => r.RoleId).ToList()
                }).Single();
        }

        public void EditUserFromAdmin(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }

}
