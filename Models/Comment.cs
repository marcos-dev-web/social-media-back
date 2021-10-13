
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models {
    public class Comment {
        [Key]
        public long Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Text { get; set; }

        [Required]
        public long PostId { get; set; }
    }
}
