using MyCvProject.Core.ViewModels.Order;
using MyCvProject.Domain.Entities.Order;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCvProject.Domain.Entities.Course;

namespace MyCvProject.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> AddOrder(string userName, int courseId, int userId);
        Task UpdatePriceOrder(int orderId);
        Task<Order> GetOrderForUserPanel(int orderId, int userId);
        Task<Order> GetOrderById(int orderId);
        Task<List<Order>> GetUserOrders(string userName, int userId);
        Task UpdateOrder(Order order);
        Task<bool> IsUserInCourse(string userName, int courseId, int userId);
        Task<DiscountUseType> UseDiscount(int orderId, string code);
        Task AddDiscount(Discount discount);
        Task<List<Discount>> GetAllDiscounts();
        Task<Discount> GetDiscountById(int discountId);
        Task UpdateDiscount(Discount discount);
        Task<bool> IsExistCode(string code);
        Task AddUserCourse(UserCourse userCourse);
    }
}
