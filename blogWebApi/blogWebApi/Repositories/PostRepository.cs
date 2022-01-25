using blogWebApi.Data;
using blogWebApi.InterfaceReposiotry;
using blogWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace blogWebApi.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _ctx;

        public PostRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            _ctx.Posts.Add(post);
            await _ctx.SaveChangesAsync();
            return post;
        }

        public async Task<Post> DeletePostAsync(int id)
        {
            var post = await _ctx.Posts.FirstOrDefaultAsync(h => h.PostId == id);
            if (post == null)
            {
                return null;
            }
            _ctx.Posts.Remove(post);

            await _ctx.SaveChangesAsync();
            return post;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _ctx.Posts.ToListAsync();
        }
   
        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _ctx.Posts.FirstOrDefaultAsync(h => h.PostId == id);

            if (post == null)
            {
                return null;
            }

            return post;


        }


        public async Task<Comment> GetPostByIdAsync(int postId, int commentId)
        {
            var comment = await _ctx.Comments.FirstOrDefaultAsync(r => r.PostId == postId && r.CommentId == commentId);

            if (comment == null)
            {
                return null;
            }
            return comment;
        }

        public async Task<Post> UpdatePostAsync(Post updatedpost)
        {
            _ctx.Posts.Update(updatedpost);
            await _ctx.SaveChangesAsync();
            return updatedpost;
        }

        public async Task<Comment> UpdatePostCommentAsync(int postId, Comment updatedComment)
        {
            _ctx.Comments.Update(updatedComment);
            await _ctx.SaveChangesAsync();

            return updatedComment;
        }


        public async Task<Comment> CreatePostCommentAsync(int postId, Comment comment)
        {
            var post = await _ctx.Posts.Include(h => h.Comments).FirstOrDefaultAsync(h => h.PostId == postId);

            post.Comments.Add(comment);
            await _ctx.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> UpdatePostComment(int postId, Comment updatedComment)
        {
            _ctx.Comments.Update(updatedComment);
            await _ctx.SaveChangesAsync();

            return updatedComment;
        }

        public async Task<Comment> DeletePostCommentAsync(int postId, int commentId)
        {
            var comment = await _ctx.Comments.SingleOrDefaultAsync(r => r.CommentId == postId && r.PostId == postId);

            if (comment == null)
            {
                return null;
            }

            _ctx.Comments.Remove(comment);

            await _ctx.SaveChangesAsync();

            return comment;
        }


        public async Task<Comment> GetPostCommentByIdAsync(int postId, int commentId)
        {
            var comment = await _ctx.Comments.FirstOrDefaultAsync(r => r.PostId == postId && r.CommentId == commentId);

            if (comment == null)
            {
                return null;
            }
            return comment;
        }
        public async Task<List<Comment>> ListPostCommentsAsync(int postId)
        {
            return await _ctx.Comments.Where(r => r.PostId == postId).ToListAsync();
        }
    }
}
