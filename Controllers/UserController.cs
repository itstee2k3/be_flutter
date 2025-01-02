using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using be_flutter_nhom2.Models;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public UserController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetUserProfile()
    {
        // Lấy userId từ Claims trong token
        var userId = User.FindFirst("userId")?.Value; // Lấy userId từ JWT claim

        if (userId == null)
        {
            return Unauthorized(new { Status = false, Message = "User not authenticated" });
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        return Ok(new 
        {
            userName = user.UserName,
            email = user.Email,
            id = user.Id
        });
    }



    // API để cập nhật thông tin người dùng
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUserProfile([FromBody] User model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        user.UserName = model.UserName;
        user.Email = model.Email;
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("Profile updated successfully.");
    }
}