    namespace be_flutter_nhom2.Models
    {
        public class Post
        {
            public int Id { get; set; }
            public string UserId { get; set; }
            public string? Title { get; set; }
            public DateTime DateCreate { get; set; }
            public string? Image { get; set; }
            public string? Description { get; set; }
            public User? User { get; set; }
            
            public bool Status { get; set; }

            public ICollection<Like>? Likes { get; set; } = new List<Like>();
            public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
        }
    }
