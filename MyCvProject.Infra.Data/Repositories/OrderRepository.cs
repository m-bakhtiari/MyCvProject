using Microsoft.EntityFrameworkCore;
using MyCvProject.Domain.Entities.Order;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.Interfaces;
using MyCvProject.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using MyCvProject.Core.ViewModels.Order;
using MyCvProject.Domain.Entities.Course;

namespace MyCvProject.Infra.Data.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly MyCvProjectContext _context;

        public OrderRepository(MyCvProjectContext context)
        {
            _context = context;
        }

        public int AddOrder(string userName, int courseId,int userId)
        {
            Order order = _context.Orders
                .FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);

            var course = _context.Courses.Find(courseId);

            if (order == null)
            {
                order = new Order()
                {
                    UserId = userId,
                    IsFinaly = false,
                    CreateDate = DateTime.Now,
                    OrderSum = course.CoursePrice,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            CourseId = courseId,
                            Count = 1,
                            Price = course.CoursePrice
                        }
                    }
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                OrderDetail detail = _context.OrderDetails
                    .FirstOrDefault(d => d.OrderId == order.OrderId && d.CourseId == courseId);
                if (detail != null)
                {
                    detail.Count += 1;
                    _context.OrderDetails.Update(detail);
                }
                else
                {
                    detail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        Count = 1,
                        CourseId = courseId,
                        Price = course.CoursePrice,
                    };
                    _context.OrderDetails.Add(detail);
                }

                _context.SaveChanges();
                UpdatePriceOrder(order.OrderId);
            }


            return order.OrderId;

        }

        public void UpdatePriceOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            order.OrderSum = _context.OrderDetails.Where(d => d.OrderId == orderId).Sum(d => d.Price);
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public Order GetOrderForUserPanel(int orderId,int userId)
        {
            return _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Course)
                .FirstOrDefault(o => o.UserId == userId && o.OrderId == orderId);
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }


        public List<Order> GetUserOrders(string userName, int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public bool IsUserInCourse(string userName, int courseId, int userId)
        {
            return _context.UserCourses.Any(c => c.UserId == userId && c.CourseId == courseId);
        }

        public DiscountUseType UseDiscount(int orderId, string code)
        {
            var discount = _context.Discounts.SingleOrDefault(d => d.DiscountCode == code);

            if (discount == null)
                return DiscountUseType.NotFound;

            if (discount.StartDate != null && discount.StartDate < DateTime.Now)
                return DiscountUseType.ExpierDate;

            if (discount.EndDate != null && discount.EndDate >= DateTime.Now)
                return DiscountUseType.ExpierDate;


            if (discount.UsableCount != null && discount.UsableCount < 1)
                return DiscountUseType.Finished;

            var order = GetOrderById(orderId);

            if (_context.UserDiscountCodes.Any(d => d.UserId == order.UserId && d.DiscountId == discount.DiscountId))
                return DiscountUseType.UserUsed;

            int percent = (order.OrderSum * discount.DiscountPercent) / 100;
            order.OrderSum = order.OrderSum - percent;

            UpdateOrder(order);

            if (discount.UsableCount != null)
            {
                discount.UsableCount -= 1;
            }

            _context.Discounts.Update(discount);
            _context.UserDiscountCodes.Add(new UserDiscountCode()
            {
                UserId = order.UserId,
                DiscountId = discount.DiscountId
            });
            _context.SaveChanges();



            return DiscountUseType.Success;
        }

        public void AddDiscount(Discount discount)
        {
            _context.Discounts.Add(discount);
            _context.SaveChanges();
        }

        public List<Discount> GetAllDiscounts()
        {
            return _context.Discounts.ToList();
        }

        public Discount GetDiscountById(int discountId)
        {
            return _context.Discounts.Find(discountId);
        }

        public void UpdateDiscount(Discount discount)
        {
            _context.Discounts.Update(discount);
            _context.SaveChanges();
        }

        public bool IsExistCode(string code)
        {
            return _context.Discounts.Any(d => d.DiscountCode == code);
        }

        public void AddUserCourse(UserCourse userCourse)
        {
            _context.UserCourses.Add(userCourse);
            _context.SaveChanges();
        }
    }
}
