using MyCvProject.Core.ViewModels;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.Entities.Wallet;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCvProject.Domain.ViewModels;

namespace MyCvProject.Core.Interfaces
{
    public interface IUserService
    {
        #region Users

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
        Task<OpRes<int>> AddUser(User user);

        /// <summary>
        /// لاگین کردن کاربر
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        Task<User> LoginUser(LoginViewModel login);

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
        Task<OpRes> UpdateUser(User user);

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
        /// حذف کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task DeleteUser(int userId); 
        #endregion

        #region User Panel

        /// <summary>
        /// گرفتن اطلاعات کامل کاربر با نام کاربری
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<InformationUserViewModel> GetUserInformation(string username);

        /// <summary>
        /// گرفتن اطلاعات کامل کاربر با آیدی
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<InformationUserViewModel> GetUserInformation(int userId);

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
        /// ویرایش پروفایل کاربر
        /// </summary>
        /// <param name="username"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        Task EditProfile(string username, EditProfileViewModel profile);

        /// <summary>
        /// آیا کاربری با نام کاربری و پسورد وجود دارد
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> CompareOldPassword(string oldPassword, string username);

        /// <summary>
        /// تغییر نام کاربری کاربر
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task ChangeUserPassword(string userName, string newPassword);

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
        /// افزایش تراکنش جدید برای بازگشت از صفحه پرداخت
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <param name="isPay"></param>
        /// <returns></returns>
        Task<int> ChargeWallet(string userName, int amount, string description, bool isPay = false);

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

        #region Admin Panel

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
        /// افزودن کاربر توسط ادمین
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<int> AddUserFromAdmin(CreateUserViewModel user);

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
        Task EditUserFromAdmin(EditUserViewModel editUser);

        #endregion
    }
}
