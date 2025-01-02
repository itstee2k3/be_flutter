using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace be_flutter_nhom2.Models;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Food> Foods { get; set; }
    public DbSet<FoodNutrient> FoodNutrients { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Consultation> Consultations { get; set; }
    public DbSet<HealthGoal> HealthGoals { get; set; }
    public DbSet<MealPlan> MealPlans { get; set; }
    public DbSet<MealPlanDetail> MealPlanDetails { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().Property(u => u.Initials).HasMaxLength(5);
        builder.HasDefaultSchema("identity");
        
        // Chỉ định khóa chính kết hợp (UserId + FoodId)
        builder.Entity<Cart>().HasKey(c => new { c.UserId, c.FoodId });

        // Cấu hình mối quan hệ Like
        builder.Entity<Like>()
            .HasOne(l => l.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(l => l.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Like>()
            .HasOne(l => l.User)
            .WithMany(u => u.Likes)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Cấu hình mối quan hệ Comment
        builder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Cấu hình mối quan hệ giữa MealPlan và MealPlanDetail
        builder.Entity<MealPlanDetail>()
            .HasOne(mp => mp.MealPlan)
            .WithMany(mp => mp.MealPlanDetails)
            .HasForeignKey(mpd => mpd.MealPlanId)
            .OnDelete(DeleteBehavior.Cascade);

        // Cấu hình mối quan hệ giữa MealPlanDetail và Food
        builder.Entity<MealPlanDetail>()
            .HasOne(mp => mp.Food)
            .WithMany()
            .HasForeignKey(mpd => mpd.FoodId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Food>()
            .HasKey(f => f.FdcId);

        // Cấu hình bảng FoodNutrient với khóa chính kết hợp giữa FdcId và Number
        builder.Entity<FoodNutrient>()
            .HasKey(fn => new {fn.Number });  // Khóa chính kết hợp FdcId và Number

        // Mối quan hệ giữa Food và FoodNutrient
        builder.Entity<FoodNutrient>()
            .HasOne(fn => fn.Food)  // FoodNutrient có một Food
            .WithMany(f => f.FoodNutrients)  // Food có nhiều FoodNutrient
            .HasForeignKey(fn => fn.FdcId)  // Khóa ngoài liên kết với FdcId của Food (đã bỏ FdcId trong FoodNutrient)
            .OnDelete(DeleteBehavior.Cascade);  // Đảm bảo khi Food bị 
    }
}
