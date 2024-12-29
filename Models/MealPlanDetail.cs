namespace be_flutter_nhom2.Models;

public class MealPlanDetail
{
    public int Id { get; set; } // Khóa chính
    public int MealPlanId { get; set; } // ID kế hoạch bữa ăn
    public MealPlan MealPlan { get; set; } // Liên kết với kế hoạch bữa ăn
    public int FoodId { get; set; } // ID thực phẩm
    public Food Food { get; set; } // Liên kết với thực phẩm
    public double Quantity { get; set; } // Số lượng thực phẩm (gram)
}