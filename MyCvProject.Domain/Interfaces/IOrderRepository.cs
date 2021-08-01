using System;
using MyCvProject.Core.ViewModels.Order;
using MyCvProject.Domain.Entities.Order;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCvProject.Domain.Entities.Course;

namespace MyCvProject.Domain.Interfaces
{
    public interface IOrderRepository : IAsyncDisposable
    {
        #region Orders

        /// <summary>
        /// گرفتن جزییات سفارش یک کاربر
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Order> GetOrderForUserPanel(int orderId, int userId);

        /// <summary>
        /// گرفتن سفارشات یک کاربر
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Order>> GetUserOrders(string userName, int userId);

        /// <summary>
        /// افزودن سفارش جدید
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> AddOrder(string userName, int courseId, int userId);

        /// <summary>
        /// ویرایش قسمت یک سفارش
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task UpdatePriceOrder(int orderId);

        /// <summary>
        /// گرفتن یک سفارش یا آیدی
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<Order> GetOrderById(int orderId);

        /// <summary>
        /// گرفتن جزییات یک سفارش یا آیدی
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<List<OrderDetail>> GetOrderDetailByOrderId(int orderId);

        /// <summary>
        /// گرفتن یک موزد از جزییات فاکترو
        /// </summary>
        /// <param name="orderDetailId"></param>
        /// <returns></returns>
        Task<OrderDetail> GetOrderDetailById(int orderDetailId);

        /// <summary>
        /// ویرایش یک سفارش
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task UpdateOrder(Order order);

        /// <summary>
        /// حذف یک مورد از فاکتور قبل از پرداخت
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        Task DeleteDetailOrder(OrderDetail orderDetail);

        /// <summary>
        /// حذف سفارش قبل از پرداخت
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task DeleteOrder(Order order);

        /// <summary>
        /// حذف  جزییات سفارش قبل از پرداخت
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        Task DeleteDetailOrder(List<OrderDetail> orderDetail);

        /// <summary>
        /// آیا فاکتور جزییات دارد
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<bool> HasOrderDetail(int orderId);

        #endregion

        #region Discounts

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
        /// گرفتن کد تخفیف با آیدی
        /// </summary>
        /// <param name="discountId"></param>
        /// <returns></returns>
        Task<Discount> GetDiscountById(int discountId);

        /// <summary>
        /// گرفتن تمام کد تخفیف های وارد شده
        /// </summary>
        /// <returns></returns>
        Task<List<Discount>> GetAllDiscounts();

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

        #region User Course

        /// <summary>
        /// وضعیت اینکه آیا کاربر از اون کد تخفیف استفاده کرده است یا خیر
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> IsUserInCourse(string userName, int courseId, int userId);

        /// <summary>
        /// افزودن کد تخفیف استفاده شده توسط کاربر
        /// </summary>
        /// <param name="userCourse"></param>
        /// <returns></returns>
        Task AddUserCourse(UserCourse userCourse);
        #endregion
    }
}
