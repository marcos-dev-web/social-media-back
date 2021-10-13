using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class Post
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        [Required]
        public int Likes { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
