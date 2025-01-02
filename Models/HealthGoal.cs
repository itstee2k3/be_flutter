namespace be_flutter_nhom2.Models;

public class HealthGoal
{
    public int Id { get; set; } // Khóa chính
    public int UserId { get; set; } // ID người dùng
    public User User { get; set; } // Liên kết với người dùng
    public string GoalType { get; set; } // Loại mục tiêu (Giảm cân, Duy trì, Tăng cân)
    public double TargetWeight { get; set; } // Cân nặng mục tiêu (kg)
    public DateTime TargetDate { get; set; } // Thời hạn đạt được mục tiêu
}