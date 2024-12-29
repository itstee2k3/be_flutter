using System.ComponentModel.DataAnnotations;

namespace be_flutter_nhom2.Models;

public class LoginModel
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
