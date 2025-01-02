namespace be_flutter_nhom2.Models;

public class MealPlan
{
    public int Id { get; set; } // Khóa chính
    public string Name { get; set; } // Tên kế hoạch
    public string UserId { get; set; } // ID người dùng
    public User? User { get; set; } // Liên kết với người dùng
    public List<MealPlanDetail> MealPlanDetails { get; set; } // Danh sách chi tiết bữa ăn
}