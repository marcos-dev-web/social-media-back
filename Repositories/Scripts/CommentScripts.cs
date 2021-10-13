
namespace SocialMedia.Repositories.Scripts {
    internal static class CommentScripts {
        public static string AddComment = @"
        IF EXISTS(SELECT * FROM [dbo].[Post] WHERE [Id] = @POST_ID)
            INSERT INTO [dbo].[Comment] (Text, PostId) VALUES (
                @TEXT,
                @POST_ID
            )
        ";
    }
}
