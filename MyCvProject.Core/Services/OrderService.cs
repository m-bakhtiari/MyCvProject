using MyCvProject.Core.Interfaces;
using MyCvProject.Core.ViewModels.Order;
using MyCvProject.Domain.Entities.Course;
using MyCvProject.Domain.Entities.Order;
using MyCvProject.Domain.Entities.Wallet;
using MyCvProject.Domain.Interfaces;
using System;
using System.Collections.Generic;

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

        public int AddOrder(string userName, int courseId)
        {
            int userId = _userService.GetUserIdByUserName(userName);

            return _orderRepository.AddOrder(userName, courseId, userId);
        }

        public void UpdatePriceOrder(int orderId)
        {
            _orderRepository.UpdatePriceOrder(orderId);
        }

        public Order GetOrderForUserPanel(string userName, int orderId)
        {
            int userId = _userService.GetUserIdByUserName(userName);

            return _orderRepository.GetOrderForUserPanel(orderId, userId);
        }

        public Order GetOrderById(int orderId)
        {
            return _orderRepository.GetOrderById(orderId);
        }

        public bool FinalyOrder(string userName, int orderId)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            var order = _orderRepository.GetOrderForUserPanel(orderId, userId);

            if (order == null || order.IsFinaly)
            {
                return false;
            }

            if (_userService.BalanceUserWallet(userName) >= order.OrderSum)
            {
                order.IsFinaly = true;
                _userService.AddWallet(new Wallet()
                {
                    Amount = order.OrderSum,
                    CreateDate = DateTime.Now,
                    IsPay = true,
                    Description = "فاکتور شما #" + order.OrderId,
                    UserId = userId,
                    TypeId = 2
                });
                _orderRepository.UpdateOrder(order);

                foreach (var detail in order.OrderDetails)
                {
                    _orderRepository.AddUserCourse(new UserCourse()
                    {
                        CourseId = detail.CourseId,
                        UserId = userId
                    });
                }

                return true;
            }

            return false;
        }

        public List<Order> GetUserOrders(string userName)
        {
            int userId = _userService.GetUserIdByUserName(userName);

            return _orderRepository.GetUserOrders(userName, userId);
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.UpdateOrder(order);
        }

        public bool IsUserInCourse(string userName, int courseId)
        {
            int userId = _userService.GetUserIdByUserName(userName);

            return _orderRepository.IsUserInCourse(userName, courseId, userId);
        }

        public DiscountUseType UseDiscount(int orderId, string code)
        {
            return _orderRepository.UseDiscount(orderId, code);
        }

        public void AddDiscount(Discount discount)
        {
            _orderRepository.AddDiscount(discount);
        }

        public List<Discount> GetAllDiscounts()
        {
            return _orderRepository.GetAllDiscounts();
        }

        public Discount GetDiscountById(int discountId)
        {
            return _orderRepository.GetDiscountById(discountId);
        }

        public void UpdateDiscount(Discount discount)
        {
            _orderRepository.UpdateDiscount(discount);
        }

        public bool IsExistCode(string code)
        {
            return _orderRepository.IsExistCode(code);
        }
    }
}
