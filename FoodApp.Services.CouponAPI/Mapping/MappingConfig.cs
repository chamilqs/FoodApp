using AutoMapper;
using FoodApp.Services.CouponAPI.Models;
using FoodApp.Services.CouponAPI.Models.DTO;

namespace FoodApp.Services.CouponAPI.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            #region Coupon
            CreateMap<Coupon, CouponDTO>()
                    .ReverseMap();
            #endregion
        }

    }
}
