using FoodApp.Web.Models;
using FoodApp.Web.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static FoodApp.Web.Utils.SD;

namespace FoodApp.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseDTO>? SendAsync(RequestDTO requestDTO)
        {

            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("FoodAppAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
    
                // token
    
                message.RequestUri = new Uri(requestDTO.Url);
                if(requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }
    
                HttpResponseMessage? apiResponse = null;
    
                switch(requestDTO.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
    
                apiResponse = await httpClient.SendAsync(message);
                
                switch(apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new(){IsSuccess = false, Message = "Not Found"};
                    case HttpStatusCode.Forbidden:
                        return new(){IsSuccess = false, Message = "Access Denied"};
                    case HttpStatusCode.Unauthorized:
                        return new(){IsSuccess = false, Message = "Unauthorized"};
                    case HttpStatusCode.InternalServerError:
                        return new(){IsSuccess = false, Message = "Internal Server Error"};
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                        return apiResponseDTO;
    
                }
            }
            catch (Exception ex)
            {
                var DTO = new ResponseDTO
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                
                return DTO;
            }


        }
    }
}
