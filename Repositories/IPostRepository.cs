﻿using be_flutter_nhom2.Models;

namespace be_flutter_nhom2.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<IEnumerable<Post>> GetByUserIdAsync(string userId); // Đổi int thành string
        Task<Post?> GetByIdAsync(int id);
        Task<Post> CreateAsync(Post post, string userId);
        Task UpdateAsync(Post post, string userId); // Thêm userId khi cập nhật
        Task DeleteAsync(int id, string userId);    // Thêm userId khi xóa

    }
}
