namespace SocialMedia.Repositories.Scripts
{
    internal static class PostScripts
    {
        public static string AddPost = @"
        INSERT INTO [dbo].[Post] (Title, Content, Likes) VALUES (
            @TITLE,
            @CONTENT,
            @LIKES
        )
        ";

        public static string GetAll = @"
        SELECT * FROM [dbo].[Post] as p
        LEFT JOIN Comment as c
        ON c.PostId = p.Id
        ";

        public static string AddLike = @"
        UPDATE Post set Likes = @LIKES WHERE Id = @POST_ID;
        ";

        public static string FindOne = @"
        SELECT * FROM [dbo].[Post] where Id = @POST_ID
        ";
    }
}
