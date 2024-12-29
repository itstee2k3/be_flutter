namespace be_flutter_nhom2.Models;

public class Comment
{
    public int Id { get; set; } // Khóa chính
    public int PostId { get; set; } // Khóa ngoại đến Post
    public string UserId { get; set; } // Khóa ngoại đến User
        
    public string Content { get; set; } // Nội dung bình luận
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Thời gian tạo bình luận
        
    // Navigation properties
    public Post? Post { get; set; }
    public User? User { get; set; }
}