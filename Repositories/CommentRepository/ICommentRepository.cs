using System.Threading.Tasks;
using SocialMedia.Models;

namespace SocialMedia.Repositories {
    public interface ICommentRepository {
        Task Create(Comment comment);
    }
}
