using MyCvProject.Core.Convertors;
using MyCvProject.Core.Generator;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Core.ViewModels;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.Entities.Wallet;
using MyCvProject.Domain.Interfaces;
using MyCvProject.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MyCvProject.Domain.Consts;
using System.Linq;

namespace MyCvProject.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<bool> IsExistUserName(string userName)
        {
            return await _userRepository.IsExistUserName(userName);
        }

        public async Task<bool> IsExistEmail(string email)
        {
            return await _userRepository.IsExistEmail(email);
        }

        public async Task<OpRes<int>> AddUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return OpRes<int>.BuildError("رمز عبور را وارد نمایید");
            }
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return OpRes<int>.BuildError("ایمیل را وارد نمایید");
            }
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                return OpRes<int>.BuildError("نام کاربری را وارد نمایید");
            }
            if (await _userRepository.IsExistEmail(user.Email))
            {
                return OpRes<int>.BuildError("ایمیل وارد شده تکراری می باشد");
            }
            if (await _userRepository.IsExistUserName(user.UserName))
            {
                return OpRes<int>.BuildError("نام کاربری وارد شده تکراری می باشد");
            }
            user.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            user.Email = FixedText.FixEmail(user.Email);
            return OpRes<int>.BuildSuccess(await _userRepository.AddUser(user));
        }

        public async Task<User> LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FixEmail(login.Email);
            return await _userRepository.LoginUser(email, hashPassword);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public async Task<User> GetUserByActiveCode(string activeCode)
        {
            return await _userRepository.GetUserByActiveCode(activeCode);
        }

        public async Task<User> GetUserByUserName(string username)
        {
            return await _userRepository.GetUserByUserName(username);
        }

        public async Task<OpRes> UpdateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return OpRes<int>.BuildError("رمز عبور را وارد نمایید");
            }
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return OpRes<int>.BuildError("ایمیل را وارد نمایید");
            }
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                return OpRes<int>.BuildError("نام کاربری را وارد نمایید");
            }
            if (await _userRepository.IsExistEmail(user.Email))
            {
                return OpRes<int>.BuildError("ایمیل وارد شده تکراری می باشد");
            }
            if (await _userRepository.IsExistUserName(user.UserName))
            {
                return OpRes<int>.BuildError("نام کاربری وارد شده تکراری می باشد");
            }
            await _userRepository.UpdateUser(user);
            return OpRes.BuildSuccess();
        }

        public async Task<bool> ActiveAccount(string activeCode)
        {
            return await _userRepository.ActiveAccount(activeCode);
        }

        public async Task<int> GetUserIdByUserName(string userName)
        {
            return await _userRepository.GetUserIdByUserName(userName);
        }

        public async Task DeleteUser(int userId)
        {
            User user = await GetUserById(userId);
            user.IsDelete = true;
            await UpdateUser(user);
        }

        public async Task<InformationUserViewModel> GetUserInformation(string username)
        {
            var user = await GetUserByUserName(username);
            InformationUserViewModel information = new InformationUserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                Wallet = await BalanceUserWallet(username)
            };

            return information;

        }

        public async Task<InformationUserViewModel> GetUserInformation(int userId)
        {
            var user = await GetUserById(userId);
            InformationUserViewModel information = new InformationUserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                Wallet = await BalanceUserWallet(user.UserName)
            };

            return information;
        }

        public async Task<SideBarUserPanelViewModel> GetSideBarUserPanelData(string username)
        {
            return await _userRepository.GetSideBarUserPanelData(username);
        }

        public async Task<EditProfileViewModel> GetDataForEditProfileUser(string username)
        {
            return await _userRepository.GetDataForEditProfileUser(username);
        }

        public async Task EditProfile(string username, EditProfileViewModel profile)
        {
            if (profile.UserAvatar != null)
            {
                string imagePath = "";
                if (profile.AvatarName != Const.DefaultUserAvatar)
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                profile.AvatarName = NameGenerator.GenerateUniqCode() + Path.GetExtension(profile.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
                await using var stream = new FileStream(imagePath, FileMode.Create);
                await profile.UserAvatar.CopyToAsync(stream);
            }

            var user = await GetUserByUserName(username);
            if (user.UserRoles.Select(x => x.RoleId).Contains(Const.RoleIdForAdmin) == false)
            {
                user.UserName = profile.UserName;
            }
            user.Email = profile.Email;
            user.UserAvatar = profile.AvatarName;

            await UpdateUser(user);
        }

        public async Task<bool> CompareOldPassword(string oldPassword, string username)
        {
            string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return await _userRepository.CompareOldPassword(hashOldPassword, username);
        }

        public async Task ChangeUserPassword(string userName, string newPassword)
        {
            var user = await GetUserByUserName(userName);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            await UpdateUser(user);
        }

        public async Task<int> BalanceUserWallet(string userName)
        {
            return await _userRepository.BalanceUserWallet(userName);
        }

        public async Task<List<WalletViewModel>> GetWalletUser(string userName)
        {
            return await _userRepository.GetWalletUser(userName);
        }

        public async Task<int> ChargeWallet(string userName, int amount, string description, bool isPay = false)
        {
            Wallet wallet = new Wallet()
            {
                Amount = amount,
                CreateDate = DateTime.Now,
                Description = description,
                IsPay = isPay,
                TypeId = 1,
                UserId = await GetUserIdByUserName(userName)
            };
            return await AddWallet(wallet);
        }

        public async Task<int> AddWallet(Wallet wallet)
        {
            return await _userRepository.AddWallet(wallet);
        }

        public async Task<Wallet> GetWalletByWalletId(int walletId)
        {
            return await _userRepository.GetWalletByWalletId(walletId);
        }

        public async Task UpdateWallet(Wallet wallet)
        {
            await _userRepository.UpdateWallet(wallet);
        }

        public async Task<UserForAdminViewModel> GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            return await _userRepository.GetUsers(pageId, filterEmail, filterUserName);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<UserForAdminViewModel> GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            return await _userRepository.GetDeleteUsers(pageId, filterEmail, filterUserName);
        }

        public async Task<int> AddUserFromAdmin(CreateUserViewModel user)
        {
            User addUser = new User
            {
                Password = PasswordHelper.EncodePasswordMd5(user.Password),
                ActiveCode = NameGenerator.GenerateUniqCode(),
                Email = user.Email,
                IsActive = true,
                RegisterDate = DateTime.Now,
                UserName = user.UserName,
                UserAvatar = Const.DefaultUserAvatar
            };

            #region Save Avatar

            if (user.UserAvatar != null)
            {
                addUser.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(user.UserAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", addUser.UserAvatar);
                await using var stream = new FileStream(imagePath, FileMode.Create);
                await user.UserAvatar.CopyToAsync(stream);
            }

            #endregion

            var result = await AddUser(addUser);
            return result.Result;

        }

        public async Task<EditUserViewModel> GetUserForShowInEditMode(int userId)
        {
           return await _userRepository.GetUserForShowInEditMode(userId);
        }

        public async Task EditUserFromAdmin(EditUserViewModel editUser)
        {
            User user = await GetUserById(editUser.UserId);
            user.Email = editUser.Email;
            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
            }

            if (editUser.UserAvatar != null)
            {
                //Delete old Image
                if (editUser.AvatarName != Const.DefaultUserAvatar)
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editUser.AvatarName);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }

                //Save New Image
                user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(editUser.UserAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.UserAvatar);
                await using var stream = new FileStream(imagePath, FileMode.Create);
                await editUser.UserAvatar.CopyToAsync(stream);
            }

            await _userRepository.EditUserFromAdmin(user);
        }

        public async Task<List<UserRole>> GetUserRoleByUserId(int userId)
        {
            return await _userRepository.GetUserRoleByUserId(userId);
        }
    }
}
