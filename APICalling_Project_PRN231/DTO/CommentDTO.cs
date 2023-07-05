using APICalling_Project_PRN231.Models;

namespace APICalling_Project_PRN231.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public int? UserId { get; set; }
        public int? ReviewId { get; set; }
        public DateTime? CommentDate { get; set; }
        public int? LikeReact { get; set; }
        public string? Comment1 { get; set; }
        public ReviewDTO? Review { get; set; }
        public UserDTO? User { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
