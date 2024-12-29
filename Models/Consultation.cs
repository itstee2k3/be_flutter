namespace be_flutter_nhom2.Models;

public class Consultation
{
    public int Id { get; set; } // Khóa chính
    public int UserId { get; set; } // ID người dùng
    public User User { get; set; } // Liên kết với người dùng
    public string Question { get; set; } // Câu hỏi của người dùng
    public string Response { get; set; } // Phản hồi từ chuyên gia hoặc hệ thống
    public DateTime CreatedAt { get; set; } // Thời gian tạo
}