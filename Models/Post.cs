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
        }
    }
