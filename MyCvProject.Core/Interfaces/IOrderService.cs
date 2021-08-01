using MyCvProject.Core.ViewModels.Order;
using MyCvProject.Domain.Entities.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.Core.Interfaces
{
    public interface IOrderService
    {
        #region Order

        /// <summary>
        /// افزودن سفارش جدید
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> AddOrder(string userName, int courseId);

        /// <summary>
        /// ویرایش قسمت یک سفارش
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task UpdatePriceOrder(int orderId);

        /// <summary>
        /// گرفتن جزییات سفارش یک کاربر
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Order> GetOrderForUserPanel(string userName, int orderId);

        /// <summary>
        /// گرفتن یک سفارش یا آیدی
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<Order> GetOrderById(int orderId);

        /// <summary>
        /// گرفتن آیدی سفارش یا آیدی جزییات آن
        /// </summary>
        /// <param name="detailId"></param>
        /// <returns></returns>
        Task<int> GetOrderByDetailId(int detailId);

        /// <summary>
        /// نهایی کردن بک خرید بعد از پرداخت
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<bool> FinalyOrder(string userName, int orderId);

        /// <summary>
        /// گرفتن سفارشات یک کاربر
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<List<Order>> GetUserOrders(string userName);

        /// <summary>
        /// ویرایش یک سفارش
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task UpdateOrder(Order order);
        #endregion

        #region User Course

        /// <summary>
        /// وضعیت اینکه آیا کاربر از اون کد تخفیف استفاده کرده است یا خیر
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        Task<bool> IsUserInCourse(string userName, int courseId);

        #endregion

        #region DisCount

        /// <summary>
        /// اعمال یک کد تخفیف بر روی یک سفارش
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<DiscountUseType> UseDiscount(int orderId, string code);

        /// <summary>
        /// افزودن کد نخفیف جدید
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        Task AddDiscount(Discount discount);

        /// <summary>
        /// حذف بکی از اقلام موجود در فاکتور قبل از پرداخت
        /// </summary>
        /// <param name="orderDetailId"></param>
        /// <returns></returns>
        Task<int> DeleteOrderDetail(int orderDetailId);

        /// <summary>
        /// گرفتن تمام کد تخفیف های وارد شده
        /// </summary>
        /// <returns></returns>
        Task<List<Discount>> GetAllDiscounts();

        /// <summary>
        /// گرفتن کد تخفیف با آیدی
        /// </summary>
        /// <param name="discountId"></param>
        /// <returns></returns>
        Task<Discount> GetDiscountById(int discountId);

        /// <summary>
        /// ویرایش کد تخفیف
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        Task UpdateDiscount(Discount discount);

        /// <summary>
        /// وضعیت معتبر بودن یک کد تخفیف
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> IsExistCode(string code);

        #endregion
    }
}
