using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using be_flutter_nhom2.DTOs;
using be_flutter_nhom2.Models;
using Newtonsoft.Json;

namespace be_flutter_nhom2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FoodController : ControllerBase
{
    private readonly HttpClient _httpClient;
    
    public FoodController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchFoods([FromQuery] string query = null)
    {
        try
        {
            var apiKey = "DOPucs0IIV0Phf3Fipj8dga3IZRh9KXaedaEQzX5"; // USDA API key
            var unsplashApiKey = "1lTjxNfFdwE3ZScobbetxwWUc8LUDm4uPMt67t-xS6I"; // Unsplash API key
            var url = $"https://api.nal.usda.gov/fdc/v1/foods/list?dataType=Survey (FNDDS)&api_key={apiKey}";

            if (!string.IsNullOrEmpty(query))
            {
                url += $"&query={Uri.EscapeDataString(query)}";
            }

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(500, "Failed to fetch data from USDA API.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var foods = JsonConvert.DeserializeObject<List<Food>>(content);

            if (foods == null || foods.Count == 0)
            {
                return Ok(new List<FoodResponse>());
            }

            // Tạo danh sách kết quả
            var result = new List<FoodResponse>();
            foreach (var food in foods)
            {
                string imageUrl = await GetImageFromUnsplash(food.Description, unsplashApiKey);

                result.Add(new FoodResponse
                {
                    FdcId = food.FdcId,
                    ImageUrl = imageUrl, // URL ảnh
                    Description = food.Description, // Lấy từ API
                    DataType = food.DataType, // Lấy từ API
                    PublicationDate = food.PublicationDate, // Lấy từ API
                    FoodCode = food.FoodCode // Lấy từ API
                });
            }

            // Trả về kết quả (giới hạn 30 thực phẩm)
            return Ok(result.Take(30));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }



    [HttpGet("details/{fdcId}")]
    public async Task<IActionResult> GetFoodDetails(int fdcId)
    {
        try
        {
            var apiKey = "DOPucs0IIV0Phf3Fipj8dga3IZRh9KXaedaEQzX5";
            var unsplashApiKey = "1lTjxNfFdwE3ZScobbetxwWUc8LUDm4uPMt67t-xS6I";

            var url = $"https://api.nal.usda.gov/fdc/v1/food/{fdcId}?format=full&api_key={apiKey}";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(500, "Failed to fetch data from USDA API.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var foodDetail = JsonConvert.DeserializeObject<Food>(content);

            if (foodDetail == null)
            {
                return NotFound("Food not found.");
            }

            // Gắn URL ảnh
            foodDetail.ImageUrl = await GetImageFromUnsplash(foodDetail.Description, unsplashApiKey);

            return Ok(foodDetail);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("default")]
    public async Task<IActionResult> GetDefaultFoods()
    {
        try
        {
            var apiKey = "DOPucs0IIV0Phf3Fipj8dga3IZRh9KXaedaEQzX5"; // USDA API key
            var defaultFoodImages = GetDefaultFoodImages(); // Danh sách các thực phẩm mặc định

            var result = new List<FoodResponse>();

            foreach (var kvp in defaultFoodImages)
            {
                var fdcId = kvp.Key;
                var imageUrl = kvp.Value;

                // Gửi yêu cầu đến USDA API để lấy thông tin chi tiết cho từng FdcId
                var url = $"https://api.nal.usda.gov/fdc/v1/food/{fdcId}?format=full&api_key={apiKey}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode(500, "Failed to fetch data from USDA API.");
                }

                var content = await response.Content.ReadAsStringAsync();
                var foodDetail = JsonConvert.DeserializeObject<Food>(content);

                if (foodDetail == null)
                {
                    continue; // Nếu không tìm thấy thông tin cho thực phẩm này, bỏ qua
                }

                // Thêm thực phẩm vào danh sách kết quả
                result.Add(new FoodResponse
                {
                    FdcId = foodDetail.FdcId,
                    ImageUrl = imageUrl, // Hình ảnh lấy từ danh sách mặc định
                    Description = foodDetail.Description,
                    DataType = foodDetail.DataType,
                    PublicationDate = foodDetail.PublicationDate,
                    FoodCode = foodDetail.FoodCode
                });
            }

            return Ok(result); // Trả về danh sách các thực phẩm với thông tin chi tiết
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    private async Task<string> GetImageFromUnsplash(string query, string unsplashApiKey)
    {
        try
        {
            var url = $"https://api.unsplash.com/search/photos?query={Uri.EscapeDataString(query)}&client_id={unsplashApiKey}";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var imageResponse = JsonConvert.DeserializeObject<UnsplashResponse>(content);

            if (imageResponse?.Results?.Count > 0)
            {
                return imageResponse.Results[0].Urls.Regular; // URL của hình ảnh đầu tiên
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    // Định nghĩa lớp để deserialize dữ liệu từ Unsplash API
    public class UnsplashResponse
    {
        [JsonProperty("results")]
        public List<UnsplashImage> Results { get; set; }
    }

    public class UnsplashImage
    {
        [JsonProperty("urls")]
        public UnsplashImageUrls Urls { get; set; }
    }

    public class UnsplashImageUrls
    {
        [JsonProperty("regular")]
        public string Regular { get; set; }
    }
    
    // Danh sách mặc định các thực phẩm với hình ảnh tĩnh
    private Dictionary<int, string> GetDefaultFoodImages()
    {
        return new Dictionary<int, string>
        {
            { 2709215, "https://example.com/images/apple.jpg" },
            { 2709224, "https://example.com/images/banana.jpg" },
            { 2709660, "https://example.com/images/carrot.jpg" },
            { 2709382, "https://example.com/images/potato.jpg" },
            // { 123460, "https://example.com/images/chicken_breast.jpg" },
            // { 123461, "https://example.com/images/rice.jpg" },
            // { 123462, "https://example.com/images/milk.jpg" },
            // { 123463, "https://example.com/images/egg.jpg" },
            // { 123464, "https://example.com/images/beef_steak.jpg" },
            // { 123465, "https://example.com/images/broccoli.jpg" }
        };
    }
}