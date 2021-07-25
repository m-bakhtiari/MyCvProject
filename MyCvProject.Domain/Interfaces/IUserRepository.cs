using MyCvProject.Core.ViewModels;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.Entities.Wallet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsExistUserName(string userName);

        Task<bool> IsExistEmail(string email);

        Task<int> AddUser(User user);

        Task<User> LoginUser(string email, string password);

        Task<User> GetUserByEmail(string email);

        Task<User> GetUserById(int userId);

        Task<User> GetUserByActiveCode(string activeCode);

        Task<User> GetUserByUserName(string username);

        Task UpdateUser(User user);

        Task<bool> ActiveAccount(string activeCode);

        Task<int> GetUserIdByUserName(string userName);

        Task<SideBarUserPanelViewModel> GetSideBarUserPanelData(string username);

        Task<EditProfileViewModel> GetDataForEditProfileUser(string username);

        Task<bool> CompareOldPassword(string oldPassword, string username);

        Task<int> BalanceUserWallet(string userName);

        Task<List<WalletViewModel>> GetWalletUser(string userName);

        Task<int> AddWallet(Wallet wallet);

        Task<Wallet> GetWalletByWalletId(int walletId);

        Task UpdateWallet(Wallet wallet);
        Task<UserForAdminViewModel> GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
        Task<List<User>> GetUsers();

        Task<UserForAdminViewModel> GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");

        Task<EditUserViewModel> GetUserForShowInEditMode(int userId);

        Task EditUserFromAdmin(User user);
    }

}

