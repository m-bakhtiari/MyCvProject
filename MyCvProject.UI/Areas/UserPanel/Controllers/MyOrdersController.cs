using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.ViewModels.Order;

namespace MyCvProject.UI.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class MyOrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public MyOrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _orderService.GetUserOrders(User.Identity.Name));
        }

        public async Task<IActionResult> ShowOrder(int id, bool finaly = false, string type = "")
        {
            var order = await _orderService.GetOrderForUserPanel(User.Identity.Name, id);

            if (order == null)
            {
                return NotFound();
            }

            ViewBag.typeDiscount = type;
            ViewBag.finaly = finaly;
            return View(order);
        }

        public async Task<IActionResult> FinalyOrder(int id)
        {
            if (await _orderService.FinalyOrder(User.Identity.Name, id))
            {
                return Redirect("/UserPanel/MyOrders/ShowOrder/" + id + "?finaly=true");
            }

            return BadRequest();
        }

        public async Task<IActionResult> UseDiscount(int orderId, string code)
        {
            DiscountUseType type = await _orderService.UseDiscount(orderId, code);
            return Redirect("/UserPanel/MyOrders/ShowOrder/" + orderId + "?type=" + type.ToString());
        }

        public async Task<IActionResult> DeleteUserOrderDetail(int id)
        {
            var orderId = await _orderService.DeleteOrderDetail(id);
            if (orderId == 0)
            {
                return Redirect("/");
            }
            return Redirect("/UserPanel/MyOrders/ShowOrder/" + orderId);
        }
    }
}