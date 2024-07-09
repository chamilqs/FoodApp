using FoodApp.Web.Models;
using FoodApp.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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

		public async Task<IActionResult> CouponCreate()
		{

            return View();
		}

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDTO couponDTO)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO? responseDTO = await _couponService.CreateCouponAsync(couponDTO);
                if (responseDTO != null && responseDTO.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }                
            }

            return View(couponDTO);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {

            ResponseDTO? responseDTO = await _couponService.GetCouponByIdAsync(couponId);
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                CouponDTO? couponDTO = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(responseDTO.Result));
                return View(couponDTO);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDTO couponDTO)
        {

            ResponseDTO? responseDTO = await _couponService.DeleteCouponAsync(couponDTO.CouponId);
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                return RedirectToAction(nameof(CouponIndex));
            }

            return View(couponDTO);
        }

    }
}
