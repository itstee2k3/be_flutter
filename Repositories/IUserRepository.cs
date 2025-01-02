using be_flutter_nhom2.Models;

namespace be_flutter_nhom2.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUserAsync();
    Task<IEnumerable<User>> GetByUserNameAsync(string userId);
    Task<User> GetByUserIdAsync(string userId);
    Task<User> UpdateUserAsync(string userId, User updatedUser);
}