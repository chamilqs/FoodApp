using AutoMapper;
using FoodApp.Services.CouponAPI.Data;
using FoodApp.Services.CouponAPI.Models;
using FoodApp.Services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FoodApp.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly CouponAPIContext _db;
        private readonly IMapper _mapper;
        private ResponseDTO _response;

        public CouponAPIController(CouponAPIContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDTO();
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(objList);

            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
                
            }

            return _response;

        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO GetById(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponId == id);
                _response.Result = _mapper.Map<CouponDTO>(obj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
            }

            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDTO GetByCode(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponCode.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDTO>(obj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
            }

            return _response;
        }

        [HttpPost]
        public ResponseDTO Post([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDTO);
                _db.Coupons.Add(obj);
                _db.SaveChanges();
                _response.Message = "Coupon was added successfully!";
                _response.Result = _mapper.Map<CouponDTO>(obj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
            }

            return _response;
        }

        [HttpPut]
        public ResponseDTO Put([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDTO);
                _db.Coupons.Update(obj);
                _db.SaveChanges();
                _response.Message = "Coupon was updated successfully!";
                _response.Result = _mapper.Map<CouponDTO>(obj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
            }

            return _response;
        }

        [HttpDelete]
        public ResponseDTO Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponId == id);
                _db.Coupons.Remove(obj);
                _db.SaveChanges();
                _response.Message = "Coupon was deleted successfully!";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
            }

            return _response;
        }


    }
}
