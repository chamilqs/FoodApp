using FoodApp.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Services.CouponAPI.Data
{
    public class CouponAPIContext : DbContext
    {
        public CouponAPIContext(DbContextOptions<CouponAPIContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
