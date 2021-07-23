using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace MyCvProject.UI.Pages.Admin.Discount
{
    public class CreateDiscountModel : PageModel
    {
        private readonly IOrderService _orderService;

        public CreateDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }



        [BindProperty]
        public Domain.Entities.Order.Discount Discount { get; set; }
        public void OnGet()
        {

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

            if (!ModelState.IsValid && await _orderService.IsExistCode(Discount.DiscountCode))
                return Page();

            await _orderService.AddDiscount(Discount);

            return RedirectToPage("Index");
        }
        //admin/discount/creatediscount?handler=checkcode
        //admin/discount/creatediscount/checkcode
        public async Task<IActionResult> OnGetCheckCode(string code)
        {
            var result = await _orderService.IsExistCode(code);
            return Content(result.ToString());
        }
    }
}