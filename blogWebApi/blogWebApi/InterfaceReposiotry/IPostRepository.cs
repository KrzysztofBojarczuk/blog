using blogWebApi.Models;

namespace blogWebApi.InterfaceReposiotry
{
    public interface IPostRepository
    {
       Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task<Post> CreatePostAsync(Post post);
        Task<Post> UpdatePostAsync(Post updatedPost);
        Task<Post> DeletePostAsync(int id);

        Task<List<Comment>> ListPostCommentsAsync(int commentId);
        Task<Comment> GetPostCommentByIdAsync(int postId, int commentId);
        Task<Comment> CreatePostCommentAsync(int postId, Comment comment);
        Task<Comment> UpdatePostComment(int postId, Comment updatedComment);

        Task<Comment> DeletePostCommentAsync(int postId, int commentId);
    }
}
