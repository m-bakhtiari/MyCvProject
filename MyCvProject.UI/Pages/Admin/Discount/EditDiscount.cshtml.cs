using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using System;
using System.Globalization;
using System.Threading.Tasks;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Consts;

namespace MyCvProject.UI.Pages.Admin.Discount
{
    [PermissionChecker(new[] { Const.PermissionIdForAdmin })]
    public class EditDiscountModel : PageModel
    {
        private readonly IOrderService _orderService;

        public EditDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public Domain.Entities.Order.Discount Discount { get; set; }

        public async Task OnGet(int id)
        {
            Discount = await _orderService.GetDiscountById(id);
        }

        public async Task<IActionResult> OnPost(string stDate = "", string edDate = "")
        {
            if (stDate != "")
            {
                string[] std = stDate.Split('/');
                Discount.StartDate = new DateTime(int.Parse(std[0]), int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar());
            }

            if (edDate != "")
            {
                string[] edd = edDate.Split('/');
                Discount.EndDate = new DateTime(int.Parse(edd[0]), int.Parse(edd[1]), int.Parse(edd[2]), new PersianCalendar());
            }

            if (!ModelState.IsValid)
                return Page();

            await _orderService.UpdateDiscount(Discount);

            return RedirectToPage("Index");

        }
    }
}