// using System.Net.Http;
// using System.Threading.Tasks;
// using Newtonsoft.Json;
//
// namespace be_flutter_nhom2.Services
// {
//     public class FoodService
//     {
//         private readonly HttpClient _httpClient;
//         private const string ApiKey = "DOPucs0IIV0Phf3Fipj8dga3IZRh9KXaedaEQzX5"; // API Key của bạn
//
//         public FoodService(HttpClient httpClient)
//         {
//             _httpClient = httpClient;
//         }
//
//         // Tìm kiếm thực phẩm từ Survey Foods (FNDDS)
//         public async Task<object> SearchSurveyFoodsAsync(string query)
//         {
//             var url = $"https://api.nal.usda.gov/fdc/v1/foods/search?query={query}&api_key={ApiKey}";
//             var response = await _httpClient.GetAsync(url);
//
//             if (!response.IsSuccessStatusCode)
//             {
//                 throw new Exception($"Error fetching data: {response.ReasonPhrase}");
//             }
//
//             var content = await response.Content.ReadAsStringAsync();
//             var foods = JsonConvert.DeserializeObject(content);
//
//             return foods;
//         }
//
//         // Lấy chi tiết thực phẩm từ FNDDS
//         public async Task<object> GetFoodDetailsAsync(int fdcId)
//         {
//             var url = $"https://api.nal.usda.gov/fdc/v1/food/{fdcId}?api_key={ApiKey}";
//             var response = await _httpClient.GetAsync(url);
//
//             if (!response.IsSuccessStatusCode)
//             {
//                 throw new Exception($"Error fetching data: {response.ReasonPhrase}");
//             }
//
//             var content = await response.Content.ReadAsStringAsync();
//             var foodDetails = JsonConvert.DeserializeObject(content);
//
//             return foodDetails;
//         }
//     }
// }