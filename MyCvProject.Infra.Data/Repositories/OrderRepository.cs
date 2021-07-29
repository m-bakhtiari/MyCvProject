using Microsoft.EntityFrameworkCore;
using MyCvProject.Domain.Entities.Order;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.Interfaces;
using MyCvProject.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCvProject.Core.ViewModels.Order;
using MyCvProject.Domain.Entities.Course;

namespace MyCvProject.Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyCvProjectContext _context;

        public OrderRepository(MyCvProjectContext context)
        {
            _context = context;
        }

        public async Task<int> AddOrder(string userName, int courseId, int userId)
        {
            Order order = await _context.Orders
                .FirstOrDefaultAsync(o => o.UserId == userId && !o.IsFinaly);

            var course = await _context.Courses.FindAsync(courseId);

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
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            else
            {
                OrderDetail detail = await _context.OrderDetails
                    .FirstOrDefaultAsync(d => d.OrderId == order.OrderId && d.CourseId == courseId);
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
                    await _context.OrderDetails.AddAsync(detail);
                }

                await _context.SaveChangesAsync();
                await UpdatePriceOrder(order.OrderId);
            }


            return order.OrderId;

        }

        public async Task UpdatePriceOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            order.OrderSum = await _context.OrderDetails.Where(d => d.OrderId == orderId).SumAsync(d => d.Price);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderForUserPanel(int orderId, int userId)
        {
            return await _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Course)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.OrderId == orderId);
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }


        public async Task<List<Order>> GetUserOrders(string userName, int userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserInCourse(string userName, int courseId, int userId)
        {
            return await _context.UserCourses.AnyAsync(c => c.UserId == userId && c.CourseId == courseId);
        }

        public async Task<DiscountUseType> UseDiscount(int orderId, string code)
        {
            var discount = await _context.Discounts.SingleOrDefaultAsync(d => d.DiscountCode == code);

            if (discount == null)
                return DiscountUseType.NotFound;

            if (discount.StartDate != null && discount.StartDate < DateTime.Now)
                return DiscountUseType.ExpierDate;

            if (discount.EndDate != null && discount.EndDate >= DateTime.Now)
                return DiscountUseType.ExpierDate;


            if (discount.UsableCount != null && discount.UsableCount < 1)
                return DiscountUseType.Finished;

            var order = await GetOrderById(orderId);

            if (await _context.UserDiscountCodes.AnyAsync(d => d.UserId == order.UserId && d.DiscountId == discount.DiscountId))
                return DiscountUseType.UserUsed;

            int percent = (order.OrderSum * discount.DiscountPercent) / 100;
            order.OrderSum = order.OrderSum - percent;

            await UpdateOrder(order);

            if (discount.UsableCount != null)
            {
                discount.UsableCount -= 1;
            }

            _context.Discounts.Update(discount);
            await _context.UserDiscountCodes.AddAsync(new UserDiscountCode()
            {
                UserId = order.UserId,
                DiscountId = discount.DiscountId
            });
            await _context.SaveChangesAsync();

            return DiscountUseType.Success;
        }

        public async Task AddDiscount(Discount discount)
        {
            await _context.Discounts.AddAsync(discount);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Discount>> GetAllDiscounts()
        {
            return await _context.Discounts.ToListAsync();
        }

        public async Task<Discount> GetDiscountById(int discountId)
        {
            return await _context.Discounts.FindAsync(discountId);
        }

        public async Task UpdateDiscount(Discount discount)
        {
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExistCode(string code)
        {
            return await _context.Discounts.AnyAsync(d => d.DiscountCode == code);
        }

        public async Task AddUserCourse(UserCourse userCourse)
        {
            await _context.UserCourses.AddAsync(userCourse);
            await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
