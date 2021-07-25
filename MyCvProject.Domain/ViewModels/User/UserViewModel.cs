namespace MyCvProject.Domain.ViewModels.User
{
    public class UserViewModel
    {
        /// <summary>
        /// آیدی کاربر
        /// فقط در حالت ویرایش پر شود
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// نام کاربری
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// ایمیل
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// رمز عبور
        /// </summary>
        public string Password { get; set; }
    }
}
