using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Dto
{
    public class Like
    {
        [Required]
        public long PostId { get; set; }
    }
}
