using System.Threading.Tasks;
using SocialMedia.Models;

using Dapper;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using SocialMedia.Repositories.Scripts;

namespace SocialMedia.Repositories
{
    public class CommentRepository : ICommentRepository
    {

        private readonly IConfiguration _configuration;

        public CommentRepository(IConfiguration configuration) {
            _configuration = configuration;
        }

        private IDbConnection DbConnection => new SqlConnection(_configuration.GetConnectionString("Connection"));

        public async Task Create(Comment comment)
        {
            IDbConnection dbConnection = DbConnection;

            dbConnection.Open();
            await dbConnection.QueryAsync<Comment>(CommentScripts.AddComment, new {
                TEXT = comment.Text,
                POST_ID = comment.PostId
            });
            dbConnection.Close();
        }
    }
}
