namespace be_flutter_nhom2.Models;

public class Cart
{
    public string UserId { get; set; }
    public int FoodId { get; set; } 
    public int Quantity { get; set; } 
    
    public Food? Food { get; set; } 
    public User? User { get; set; }
}