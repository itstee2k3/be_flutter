namespace be_flutter_nhom2.Models;

public class Like
{
    public int Id { get; set; } // Khóa chính
    public int PostId { get; set; } // Khóa ngoại đến Post
    public string UserId { get; set; } // Khóa ngoại đến User
        
    public Post? Post { get; set; }
    public User? User { get; set; }
        
    public DateTime LikedAt { get; set; } = DateTime.UtcNow; // Thời gian tạo like
}