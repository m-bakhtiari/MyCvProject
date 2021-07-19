using MyCvProject.Core.ViewModels.Order;
using MyCvProject.Domain.Entities.Order;
using System.Collections.Generic;
using MyCvProject.Domain.Entities.Course;

namespace MyCvProject.Domain.Interfaces
{
    public interface IOrderRepository
   {
       int AddOrder(string userName, int courseId, int userId);
       void UpdatePriceOrder(int orderId);
       Order GetOrderForUserPanel(int orderId, int userId);
       Order GetOrderById(int orderId);
       List<Order> GetUserOrders(string userName, int userId);
       void UpdateOrder(Order order);
       bool IsUserInCourse(string userName, int courseId, int userId);
       DiscountUseType UseDiscount(int orderId, string code);
       void AddDiscount(Discount discount);
       List<Discount> GetAllDiscounts();
       Discount GetDiscountById(int discountId);
       void UpdateDiscount(Discount discount);
       bool IsExistCode(string code);
       void AddUserCourse(UserCourse userCourse);
   }
}
