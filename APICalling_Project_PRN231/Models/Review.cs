using System;
using System.Collections.Generic;

namespace APICalling_Project_PRN231.Models
{
    public partial class Review
    {
        public Review()
        {
            Comments = new HashSet<Comment>();
        }

        public int ReviewId { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int? Rating { get; set; }
        public DateTime? ReviewDate { get; set; }
        public int? LikeReact { get; set; }
        public string? Content { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
