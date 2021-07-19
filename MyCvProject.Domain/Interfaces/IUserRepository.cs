using MyCvProject.Core.ViewModels;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.Entities.Wallet;
using System.Collections.Generic;

namespace MyCvProject.Domain.Interfaces
{
    public interface IUserRepository
    {
        bool IsExistUserName(string userName);

        bool IsExistEmail(string email);

        int AddUser(User user);

        User LoginUser(string email, string password);

        User GetUserByEmail(string email);

        User GetUserById(int userId);

        User GetUserByActiveCode(string activeCode);

        User GetUserByUserName(string username);

        void UpdateUser(User user);

        bool ActiveAccount(string activeCode);

        int GetUserIdByUserName(string userName);

        SideBarUserPanelViewModel GetSideBarUserPanelData(string username);

        EditProfileViewModel GetDataForEditProfileUser(string username);

        bool CompareOldPassword(string oldPassword, string username);

        int BalanceUserWallet(string userName);

        List<WalletViewModel> GetWalletUser(string userName);

        int AddWallet(Wallet wallet);

        Wallet GetWalletByWalletId(int walletId);

        void UpdateWallet(Wallet wallet);
        UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");

        UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");

        EditUserViewModel GetUserForShowInEditMode(int userId);

        void EditUserFromAdmin(User user);
    }

}

