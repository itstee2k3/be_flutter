using be_flutter_nhom2.Models;

namespace be_flutter_nhom2.Repositories;

public interface ICartRepository
{
    Task<List<Cart>> GetCartByIdUserAsync(string idUser);

    Task<Cart> AddFoodToCartAsync(string idUser, int idFood, int quantity);
    
    Task<Cart> RemoveFoodFromCartAsync(string idUser, int idFood);
    
    Task<Cart> UpdateFoodQuantityAsync(string idUser, int idFood, int newQuantity);

    Task ClearCartAsync(string idUser);
    
    Task<double> GetCartTotalAsync(string idUser);
}
