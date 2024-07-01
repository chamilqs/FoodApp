using FoodApp.Web.Models;
using FoodApp.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FoodApp.Web.Controllers
{
    public class CouponController : Controller
    {
        public readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;            
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDTO> list = new();

            ResponseDTO? responseDTO = await _couponService.GetAllCouponsAsync();
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(responseDTO.Result));

            }

            return View(list);
        }
    }
}
