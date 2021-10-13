using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMedia.Models;
using SocialMedia.Dto;

namespace SocialMedia.Repositories
{
    public interface IPostRespository
    {
        Task Create(Post post);
        Task AddLike(Like like);
        Task<IEnumerable<Post>> GetAll();
    }
}
