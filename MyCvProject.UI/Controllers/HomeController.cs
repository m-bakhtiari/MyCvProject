using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCvProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyCvProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;

        public HomeController(IUserService userService, ICourseService courseService)
        {
            _userService = userService;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var popular = _courseService.GetPopularCourse();
            ViewBag.PopularCourse = popular;
            var model = await _courseService.GetCourse();
            return View(model.Item1);
        }



        [Route("OnlinePayment/{id}")]
        public async Task<IActionResult> onlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                var wallet = await _userService.GetWalletByWalletId(id);

                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    await _userService.UpdateWallet(wallet);
                }
            }

            return View();
        }

        public async Task<IActionResult> GetSubGroups(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };
            list.AddRange(await _courseService.GetSubGroupForManageCourse(id));
            return Json(new SelectList(list, "Value", "Text"));
        }

        [HttpPost]
        [Route("file-upload")]
        public async Task<IActionResult> UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;
            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MyImages", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await upload.CopyToAsync(stream);
            }
            var url = $"{"/MyImages/"}{fileName}";
            return Json(new { uploaded = true, url });
        }
    }
}