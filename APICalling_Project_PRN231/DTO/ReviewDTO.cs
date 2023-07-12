using APICalling_Project_PRN231.Models;

namespace APICalling_Project_PRN231.DTO
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int? Rating { get; set; }
        public DateTime? ReviewDate { get; set; }
        public int? LikeReact { get; set; }
        public string? Content { get; set; }
        public bool? IsActive { get; set; }

        public UserDTO? User { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public int totalComment { get; set; }
    }
}
