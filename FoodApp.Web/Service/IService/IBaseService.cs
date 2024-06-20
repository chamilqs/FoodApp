using FoodApp.Web.Models;

namespace FoodApp.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDTO>? SendAsync(RequestDTO requestDTO);
    }
}
