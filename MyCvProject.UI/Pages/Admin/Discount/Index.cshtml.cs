using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using System.Collections.Generic;

namespace MyCvProject.UI.Pages.Admin.Discount
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public List<Domain.Entities.Order.Discount> Discounts { get; set; }
        public void OnGet()
        {
            Discounts = _orderService.GetAllDiscounts();
        }
    }
}