using MyCvProject.Core.ViewModels.Order;
using MyCvProject.Domain.Entities.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.Core.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddOrder(string userName, int courseId);
        Task UpdatePriceOrder(int orderId);
        Task<Order> GetOrderForUserPanel(string userName, int orderId);
        Task<Order> GetOrderById(int orderId);
        Task<bool> FinalyOrder(string userName, int orderId);
        Task<List<Order>> GetUserOrders(string userName);
        Task UpdateOrder(Order order);
        Task<bool> IsUserInCourse(string userName, int courseId);
        #region DisCount
        Task<DiscountUseType> UseDiscount(int orderId, string code);
        Task AddDiscount(Discount discount);
        Task<List<Discount>> GetAllDiscounts();
        Task<Discount> GetDiscountById(int discountId);
        Task UpdateDiscount(Discount discount);
        Task<bool> IsExistCode(string code);

        #endregion
    }
}
