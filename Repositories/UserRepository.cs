using be_flutter_nhom2.Models;
using Microsoft.EntityFrameworkCore;

namespace be_flutter_nhom2.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Lấy danh sách tất cả người dùng
    public async Task<IEnumerable<User>> GetAllUserAsync()
    {
        return await _context.Users.ToListAsync();
    }

    // Lấy danh sách người dùng theo username
    public async Task<IEnumerable<User>> GetByUserNameAsync(string userName)
    {
        return await _context.Users
            .Where(u => u.UserName.Contains(userName))
            .ToListAsync();
    }

    // Lấy thông tin người dùng theo userId
    public async Task<User?> GetByUserIdAsync(string userId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    // Cập nhật thông tin người dùng
    public async Task<User> UpdateUserAsync(string userId, User updatedUser)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            throw new KeyNotFoundException($"User with ID {userId} not found.");

        user.UserName = updatedUser.UserName;
        user.Email = updatedUser.Email;
        user.PhoneNumber = updatedUser.PhoneNumber;
        // Các trường khác cũng cần cập nhật theo nhu cầu

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return user;
    }
}