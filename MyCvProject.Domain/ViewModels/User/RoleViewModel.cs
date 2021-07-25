namespace MyCvProject.Domain.ViewModels.User
{
    public class RoleViewModel
    {
        /// <summary>
        /// آیدی نقش
        /// فقط در حالت افزودن نقش جدید پر شود
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// عنوان نقش
        /// </summary>
        public string RoleTitle { get; set; }
    }
}
