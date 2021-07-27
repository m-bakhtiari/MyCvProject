using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Consts;

namespace MyCvProject.UI.Pages.Admin.Discount
{
    [PermissionChecker(new[] { Const.PermissionIdForAdmin })]
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public List<Domain.Entities.Order.Discount> Discounts { get; set; }
        public async Task OnGet()
        {
            Discounts = await _orderService.GetAllDiscounts();
        }
    }
}