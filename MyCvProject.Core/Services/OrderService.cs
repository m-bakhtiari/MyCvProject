using MyCvProject.Core.Interfaces;
using MyCvProject.Core.ViewModels.Order;
using MyCvProject.Domain.Entities.Course;
using MyCvProject.Domain.Entities.Order;
using MyCvProject.Domain.Entities.Wallet;
using MyCvProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserService _userService;

        public OrderService(IOrderRepository orderRepository, IUserService userService)
        {
            _orderRepository = orderRepository;
            _userService = userService;
        }

        public async Task<int> AddOrder(string userName, int courseId)
        {
            int userId = await _userService.GetUserIdByUserName(userName);

            return await _orderRepository.AddOrder(userName, courseId, userId);
        }

        public async Task UpdatePriceOrder(int orderId)
        {
            await _orderRepository.UpdatePriceOrder(orderId);
        }

        public async Task<Order> GetOrderForUserPanel(string userName, int orderId)
        {
            int userId = await _userService.GetUserIdByUserName(userName);

            return await _orderRepository.GetOrderForUserPanel(orderId, userId);
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _orderRepository.GetOrderById(orderId);
        }

        public async Task<int> GetOrderByDetailId(int detailId)
        {
            var detail = await _orderRepository.GetOrderDetailById(detailId);
            return detail.OrderId;
        }

        public async Task<bool> FinalyOrder(string userName, int orderId)
        {
            int userId = await _userService.GetUserIdByUserName(userName);
            var order = await _orderRepository.GetOrderForUserPanel(orderId, userId);

            if (order == null || order.IsFinaly)
            {
                return false;
            }

            if (await _userService.BalanceUserWallet(userName) >= order.OrderSum)
            {
                order.IsFinaly = true;
                await _userService.AddWallet(new Wallet()
                {
                    Amount = order.OrderSum,
                    CreateDate = DateTime.Now,
                    IsPay = true,
                    Description = "فاکتور شما #" + order.OrderId,
                    UserId = userId,
                    TypeId = 2
                });
                await _orderRepository.UpdateOrder(order);

                foreach (var detail in order.OrderDetails)
                {
                    await _orderRepository.AddUserCourse(new UserCourse()
                    {
                        CourseId = detail.CourseId,
                        UserId = userId
                    });
                }

                return true;
            }

            return false;
        }

        public async Task<List<Order>> GetUserOrders(string userName)
        {
            int userId = await _userService.GetUserIdByUserName(userName);

            return await _orderRepository.GetUserOrders(userName, userId);
        }

        public async Task UpdateOrder(Order order)
        {
            await _orderRepository.UpdateOrder(order);
        }

        public async Task DeleteOrder(int orderId)
        {
            var orderDetail = await _orderRepository.GetOrderDetailByOrderId(orderId);
            if (orderDetail == null)
                return;
            await _orderRepository.DeleteDetailOrder(orderDetail);
            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null)
                return;
            await _orderRepository.DeleteOrder(order);
        }

        public async Task<bool> IsUserInCourse(string userName, int courseId)
        {
            int userId = await _userService.GetUserIdByUserName(userName);

            return await _orderRepository.IsUserInCourse(userName, courseId, userId);
        }

        public async Task<DiscountUseType> UseDiscount(int orderId, string code)
        {
            return await _orderRepository.UseDiscount(orderId, code);
        }

        public async Task AddDiscount(Discount discount)
        {
            await _orderRepository.AddDiscount(discount);
        }

        public async Task<List<Discount>> GetAllDiscounts()
        {
            return await _orderRepository.GetAllDiscounts();
        }

        public async Task<Discount> GetDiscountById(int discountId)
        {
            return await _orderRepository.GetDiscountById(discountId);
        }

        public async Task UpdateDiscount(Discount discount)
        {
            await _orderRepository.UpdateDiscount(discount);
        }

        public async Task<bool> IsExistCode(string code)
        {
            return await _orderRepository.IsExistCode(code);
        }

        public async Task<int> DeleteOrderDetail(int orderDetailId)
        {
            var detail = await _orderRepository.GetOrderDetailById(orderDetailId);
            var orderId = detail.OrderId;
            await _orderRepository.DeleteDetailOrder(detail);
            await _orderRepository.UpdatePriceOrder(orderId);
            if (await _orderRepository.HasOrderDetail(detail.OrderId) == false)
            {
                await _orderRepository.DeleteOrder(detail.Order);
                return 0;
            }
            return orderId;
        }
    }
}
