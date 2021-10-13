using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using SocialMedia.Models;
using SocialMedia.Repositories.Scripts;
using SocialMedia.Dto;

namespace SocialMedia.Repositories
{
    public class PostRepository : IPostRespository
    {
        private readonly IConfiguration _configuration;

        public PostRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection DbConnection =>
            new SqlConnection(_configuration.GetConnectionString("Connection"));

        public async Task Create(Post post)
        {
            IDbConnection dbConnection = DbConnection;

            dbConnection.Open();

            await dbConnection
                .QueryAsync<Post>(PostScripts.AddPost,
                new { TITLE = post.Title, CONTENT = post.Content, LIKES = 0 });

            dbConnection.Close();
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            IDbConnection dbConnection = DbConnection;

            List<Post> posts = new List<Post>();

            dbConnection.Open();
            IEnumerable<Post> result =
                await dbConnection
                    .QueryAsync<Post, Comment, Post>(PostScripts.GetAll,
                    map: (posts, comments) =>
                    {
                        if (comments != null)
                        {
                            posts.Comments.Add (comments);
                        }

                        return posts;
                    },
                    splitOn: "Id,Id");
            
            foreach (Post post in result) {
                if (posts.Find(x => x.Id == post.Id) == null) {
                    posts.Add(post);
                }
            }

            foreach (Post post in posts) {
                foreach(Post p in result) {
                    foreach (Comment comment in p.Comments) {
                        if (post.Comments.Find(x => x.Id == comment.Id) == null) {
                            if (post.Id == comment.PostId) {
                                post.Comments.Add(comment);
                            }
                        }
                    }
                }
            }

            return posts;
        }
    
        public async Task AddLike(Like like) {
            IDbConnection dbConnection = DbConnection;

            dbConnection.Open();

            IEnumerable<Post> posts = await dbConnection.QueryAsync<Post>(PostScripts.FindOne, new {
                POST_ID = like.PostId
            });

            if (posts.ToArray().Length > 0)
            {
                await dbConnection.QueryAsync(PostScripts.AddLike, new{
                    POST_ID = like.PostId,
                    LIKES = posts.ToList()[0].Likes + 1,
                });
            }
        }
    }
}
