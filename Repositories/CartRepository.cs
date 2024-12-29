using be_flutter_nhom2.Models;
using Microsoft.EntityFrameworkCore;

namespace be_flutter_nhom2.Repositories;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // 1. Lấy giỏ hàng của người dùng theo UserId
    public async Task<List<Cart>> GetCartByIdUserAsync(string idUser)
    {
        var carts = await _context.Carts
            .Include(c => c.Food)  // Bao gồm thông tin sản phẩm
            .Where(c => c.UserId == idUser)  // Lấy tất cả sản phẩm trong giỏ hàng của người dùng
            .ToListAsync();  // Trả về danh sách giỏ hàng của người dùng

        return carts;  // Trả về danh sách các sản phẩm trong giỏ hàng
    }


    // 2. Thêm sản phẩm vào giỏ hàng
    public async Task<Cart> AddFoodToCartAsync(string idUser, int idFood, int quantity)
    {
        var cart = await _context.Carts
            .Include(c => c.Food)  // Bao gồm thông tin sản phẩm
            .FirstOrDefaultAsync(c => c.UserId == idUser && c.FoodId == idFood);

        // Nếu sản phẩm chưa có trong giỏ hàng của người dùng, tạo mới
        if (cart == null)
        {
            cart = new Cart
            {
                UserId = idUser.ToString(),
                FoodId = idFood,
                Quantity = quantity
            };
            _context.Carts.Add(cart);
        }
        else
        {
            // Nếu sản phẩm đã có trong giỏ, chỉ cần tăng số lượng
            cart.Quantity += quantity;
        }

        await _context.SaveChangesAsync();  // Lưu thay đổi
        return cart;  // Trả về giỏ hàng đã cập nhật
    }

    // 3. Xóa sản phẩm khỏi giỏ hàng
    public async Task<Cart> RemoveFoodFromCartAsync(string idUser, int idFood)
    {
        var cart = await _context.Carts
            .FirstOrDefaultAsync(c => c.UserId == idUser && c.FoodId == idFood);

        if (cart != null)
        {
            _context.Carts.Remove(cart);  // Xóa sản phẩm khỏi giỏ hàng
            await _context.SaveChangesAsync();
        }

        return cart;  // Trả về giỏ hàng đã xóa sản phẩm
    }

    // 4. Cập nhật số lượng sản phẩm trong giỏ hàng
    public async Task<Cart> UpdateFoodQuantityAsync(string idUser, int idFood, int newQuantity)
    {
        var cart = await _context.Carts
            .FirstOrDefaultAsync(c => c.UserId == idUser && c.FoodId == idFood);

        if (cart != null)
        {
            cart.Quantity = newQuantity;  // Cập nhật số lượng
            await _context.SaveChangesAsync();
        }

        return cart;  // Trả về giỏ hàng sau khi cập nhật
    }

    // 5. Xóa toàn bộ giỏ hàng của người dùng
    public async Task ClearCartAsync(string idUser)
    {
        var cartItems = await _context.Carts
            .Where(c => c.UserId == idUser)
            .ToListAsync();  // Lấy tất cả các sản phẩm trong giỏ hàng

        _context.Carts.RemoveRange(cartItems);  // Xóa tất cả sản phẩm khỏi giỏ hàng
        await _context.SaveChangesAsync();
    }

    // 6. Tính tổng giá trị giỏ hàng
    public async Task<double> GetCartTotalAsync(string idUser)
    {
        var cartItems = await _context.Carts
            .Include(c => c.Food)  // Bao gồm thông tin sản phẩm
            .Where(c => c.UserId == idUser)
            .ToListAsync();  // Lấy tất cả sản phẩm trong giỏ hàng
    
        return cartItems.Sum(ci => ci.Quantity * ci.Food.Calories);  // Tính tổng giá trị giỏ hàng
    }
}
