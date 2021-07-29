﻿using MyCvProject.Core.ViewModels;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.Entities.Wallet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.Domain.Interfaces
{
    public interface IUserRepository
    {
        #region User

        /// <summary>
        /// بررسی تکراری بودن نام کاربری
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> IsExistUserName(string userName);

        /// <summary>
        /// آیا ایمیل تکراری است یا خیر
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsExistEmail(string email);

        /// <summary>
        /// افزودن کاربر جدید
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<int> AddUser(User user);

        /// <summary>
        /// لاگین کردن کاربر
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> LoginUser(string email, string password);

        /// <summary>
        /// گرفتن کاربر با ایمیل
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User> GetUserByEmail(string email);

        /// <summary>
        /// گرفتن کاربر با آیدی
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> GetUserById(int userId);

        /// <summary>
        /// گرفتن کاربر با کد فعالسازی
        /// </summary>
        /// <param name="activeCode"></param>
        /// <returns></returns>
        Task<User> GetUserByActiveCode(string activeCode);

        /// <summary>
        /// گرفتن کاربر با نام کاربری
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<User> GetUserByUserName(string username);

        /// <summary>
        /// ویرایش کاربر
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task UpdateUser(User user);

        /// <summary>
        /// فعال کردن اکانت کاربر
        /// </summary>
        /// <param name="activeCode"></param>
        /// <returns></returns>
        Task<bool> ActiveAccount(string activeCode);

        /// <summary>
        /// گرفتن آیدی کاربر با نام کاربری
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<int> GetUserIdByUserName(string userName);

        /// <summary>
        /// گرفتن اطلاعات کاربر برای پروفایل کاربر
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<SideBarUserPanelViewModel> GetSideBarUserPanelData(string username);

        /// <summary>
        /// گرفتن اطلاعات کاربر برای ویرایش توسط کاربر
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<EditProfileViewModel> GetDataForEditProfileUser(string username);

        /// <summary>
        /// آیا کاربری با نام کاربری و پسورد وجود دارد
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> CompareOldPassword(string oldPassword, string username);

        /// <summary>
        /// گرفتن تمام کاربران با امکان فیلتر و صفحه بندی
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="filterEmail"></param>
        /// <param name="filterUserName"></param>
        /// <returns></returns>
        Task<UserForAdminViewModel> GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");

        /// <summary>
        /// گرفتن تمام لیست کاربران
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetUsers();

        /// <summary>
        /// گرفتن تمام کاربران حذف شده با صفحه بندی و امکان فیلتر
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="filterEmail"></param>
        /// <param name="filterUserName"></param>
        /// <returns></returns>
        Task<UserForAdminViewModel> GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");

        /// <summary>
        /// گرفتن اطلاعات کاربر برای ویرایش در حالت ادمین
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<EditUserViewModel> GetUserForShowInEditMode(int userId);

        /// <summary>
        /// ویرایش کاربر
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task EditUserFromAdmin(User user);
        #endregion

        #region Wallet

        /// <summary>
        /// مقدار پول باقیمانده کاربر
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<int> BalanceUserWallet(string userName);

        /// <summary>
        /// تمام تراکنش های کاربر
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<List<WalletViewModel>> GetWalletUser(string userName);

        /// <summary>
        /// افزودن تراکنش جدید
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns></returns>
        Task<int> AddWallet(Wallet wallet);

        /// <summary>
        /// گرفتن کیف پول با آیدی
        /// </summary>
        /// <param name="walletId"></param>
        /// <returns></returns>
        Task<Wallet> GetWalletByWalletId(int walletId);

        /// <summary>
        /// ویرایش کیف پول
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns></returns>
        Task UpdateWallet(Wallet wallet); 
        #endregion
    }

}

