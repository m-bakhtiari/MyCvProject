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
        Task<bool> IsExistUserName(string userName);
        Task<bool> IsExistEmail(string email);
        Task<OpRes<int>> AddUser(User user);
        Task<User> LoginUser(LoginViewModel login);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int userId);
        Task<User> GetUserByActiveCode(string activeCode);
        Task<User> GetUserByUserName(string username);
        Task<OpRes> UpdateUser(User user);
        Task<bool> ActiveAccount(string activeCode);
        Task<int> GetUserIdByUserName(string userName);
        Task DeleteUser(int userId);

        #region User Panel

        Task<InformationUserViewModel> GetUserInformation(string username);
        Task<InformationUserViewModel> GetUserInformation(int userId);
        Task<SideBarUserPanelViewModel> GetSideBarUserPanelData(string username);
        Task<EditProfileViewModel> GetDataForEditProfileUser(string username);
        Task EditProfile(string username, EditProfileViewModel profile);
        Task<bool> CompareOldPassword(string oldPassword, string username);
        Task ChangeUserPassword(string userName, string newPassword);

        #endregion

        #region Wallet

        Task<int> BalanceUserWallet(string userName);
        Task<List<WalletViewModel>> GetWalletUser(string userName);
        Task<int> ChargeWallet(string userName, int amount, string description, bool isPay = false);
        Task<int> AddWallet(Wallet wallet);
        Task<Wallet> GetWalletByWalletId(int walletId);
        Task UpdateWallet(Wallet wallet);

        #endregion

        #region Admin Panel

        Task<UserForAdminViewModel> GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
        Task<List<User>> GetUsers();
        Task<UserForAdminViewModel> GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
        Task<int> AddUserFromAdmin(CreateUserViewModel user);
        Task<EditUserViewModel> GetUserForShowInEditMode(int userId);
        Task EditUserFromAdmin(EditUserViewModel editUser);

        #endregion
    }
}
