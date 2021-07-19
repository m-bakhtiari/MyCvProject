using MyCvProject.Core.Convertors;
using MyCvProject.Core.Generator;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Core.ViewModels;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.Entities.Wallet;
using MyCvProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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


        public bool IsExistUserName(string userName)
        {
            return _userRepository.IsExistUserName(userName);
        }

        public bool IsExistEmail(string email)
        {
            return _userRepository.IsExistEmail(email);
        }

        public int AddUser(User user)
        {
            return _userRepository.AddUser(user);
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FixEmail(login.Email);
            return _userRepository.LoginUser(email, hashPassword);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _userRepository.GetUserByActiveCode(activeCode);
        }

        public User GetUserByUserName(string username)
        {
            return _userRepository.GetUserByUserName(username);
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }

        public bool ActiveAccount(string activeCode)
        {
            return _userRepository.ActiveAccount(activeCode);
        }

        public int GetUserIdByUserName(string userName)
        {
            return _userRepository.GetUserIdByUserName(userName);
        }

        public void DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            user.IsDelete = true;
            UpdateUser(user);
        }

        public InformationUserViewModel GetUserInformation(string username)
        {
            var user = GetUserByUserName(username);
            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            information.Wallet = BalanceUserWallet(username);

            return information;

        }

        public InformationUserViewModel GetUserInformation(int userId)
        {
            var user = GetUserById(userId);
            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            information.Wallet = BalanceUserWallet(user.UserName);

            return information;
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
        {
            return _userRepository.GetSideBarUserPanelData(username);
        }

        public EditProfileViewModel GetDataForEditProfileUser(string username)
        {
            return _userRepository.GetDataForEditProfileUser(username);
        }

        public void EditProfile(string username, EditProfileViewModel profile)
        {
            if (profile.UserAvatar != null)
            {
                string imagePath = "";
                if (profile.AvatarName != "Defult.jpg")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                profile.AvatarName = NameGenerator.GenerateUniqCode() + Path.GetExtension(profile.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    profile.UserAvatar.CopyTo(stream);
                }

            }

            var user = GetUserByUserName(username);
            user.UserName = profile.UserName;
            user.Email = profile.Email;
            user.UserAvatar = profile.AvatarName;

            UpdateUser(user);

        }

        public bool CompareOldPassword(string oldPassword, string username)
        {
            string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _userRepository.CompareOldPassword(hashOldPassword, username);
        }

        public void ChangeUserPassword(string userName, string newPassword)
        {
            var user = GetUserByUserName(userName);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            UpdateUser(user);
        }

        public int BalanceUserWallet(string userName)
        {
            return _userRepository.BalanceUserWallet(userName);
        }

        public List<WalletViewModel> GetWalletUser(string userName)
        {
            return _userRepository.GetWalletUser(userName);
        }

        public int ChargeWallet(string userName, int amount, string description, bool isPay = false)
        {
            Wallet wallet = new Wallet()
            {
                Amount = amount,
                CreateDate = DateTime.Now,
                Description = description,
                IsPay = isPay,
                TypeId = 1,
                UserId = GetUserIdByUserName(userName)
            };
            return AddWallet(wallet);
        }

        public int AddWallet(Wallet wallet)
        {
            return _userRepository.AddWallet(wallet);
        }

        public Wallet GetWalletByWalletId(int walletId)
        {
            return _userRepository.GetWalletByWalletId(walletId);
        }

        public void UpdateWallet(Wallet wallet)
        {
            _userRepository.UpdateWallet(wallet);
        }

        public UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            return _userRepository.GetUsers(pageId, filterEmail, filterUserName);
        }

        public UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            return _userRepository.GetDeleteUsers(pageId, filterEmail, filterUserName);
        }

        public int AddUserFromAdmin(CreateUserViewModel user)
        {
            User addUser = new User();
            addUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            addUser.ActiveCode = NameGenerator.GenerateUniqCode();
            addUser.Email = user.Email;
            addUser.IsActive = true;
            addUser.RegisterDate = DateTime.Now;
            addUser.UserName = user.UserName;

            #region Save Avatar

            if (user.UserAvatar != null)
            {
                string imagePath = "";
                addUser.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(user.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", addUser.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(stream);
                }
            }

            #endregion

            return AddUser(addUser);

        }

        public EditUserViewModel GetUserForShowInEditMode(int userId)
        {
            return _userRepository.GetUserForShowInEditMode(userId);
        }

        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
            User user = GetUserById(editUser.UserId);
            user.Email = editUser.Email;
            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
            }

            if (editUser.UserAvatar != null)
            {
                //Delete old Image
                if (editUser.AvatarName != "Defult.jpg")
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
                using var stream = new FileStream(imagePath, FileMode.Create);
                editUser.UserAvatar.CopyTo(stream);
            }

            _userRepository.EditUserFromAdmin(user);
        }
    }
}
