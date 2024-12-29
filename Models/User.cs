using Microsoft.AspNetCore.Identity;

namespace be_flutter_nhom2.Models;

public class User : IdentityUser
{
    public string? FullName { get; set; } 
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? Avatar { get; set; }
    public string? Initials { get; set; }
    
    public DateTime? DateOfBirth { get; set; }

    public double? Height { get; set; } // Chiều cao (cm)
    
    public double? Weight { get; set; } // Cân nặng (kg)
    
    public string? Gender { get; set; } // Giới tính (Nam, Nữ, Khác)
    
    public string? ActivityLevel { get; set; } // Mức độ hoạt động (Ít vận động, Trung bình, Năng động)
    
    public ICollection<Like> Likes { get; set; } = new List<Like>();

    public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
}
