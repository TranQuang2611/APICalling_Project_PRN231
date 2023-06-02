using System;
using System.Collections.Generic;

namespace APICalling_Project_PRN231.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int? UserId { get; set; }
        public int? ReviewId { get; set; }
        public DateTime? CommentDate { get; set; }
        public int? LikeReact { get; set; }
        public string? Comment1 { get; set; }

        public virtual Review? Review { get; set; }
        public virtual User? User { get; set; }
    }
}
