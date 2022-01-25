using AutoMapper;
using blogWebApi.Dtos;
using blogWebApi.InterfaceReposiotry;
using blogWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            var postsGet = _mapper.Map<List<PostGetDto>>(posts);
            return Ok(postsGet);
            
        }
        [HttpGet]
        [Route("{postId}/comments")]
        public async Task<IActionResult> GetAllPostComments(int postId)
        {
            var comments = await _postRepository.ListPostCommentsAsync(postId);
            var mappedComment = _mapper.Map<List<CommentGetDto>>(comments);

            return Ok(mappedComment);
        }
        [HttpGet]
        [Route("{postId}/comments/{commentId}")]
        public async Task<IActionResult> GetPostCommentById(int postId, int commentId)
        {
            var comment = await _postRepository.GetPostCommentByIdAsync(postId, commentId);

            var mappedComment = _mapper.Map<CommentGetDto>(comment);
            return Ok(mappedComment);
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            var postGet = _mapper.Map<PostGetDto>(post);
            return Ok(postGet);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateDto post)
        {
            var domainPost = _mapper.Map<Post>(post);
            await _postRepository.CreatePostAsync(domainPost);

            var postGet = _mapper.Map<PostGetDto>(domainPost);
            return CreatedAtAction(nameof(GetPostById), new { id = domainPost.PostId}, postGet);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePost([FromBody] PostCreateDto updated, int id)
        {
            var toUpdated = _mapper.Map<Post>(updated);
            toUpdated.PostId = id;
            await _postRepository.UpdatePostAsync(toUpdated);

            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _postRepository.DeletePostAsync(id);

            if (post == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost]
        [Route("{postId}/comments")]
        public async Task<IActionResult> AddCommentPost(int postId, [FromBody] CommentCreateDto newComment)
        {
            var comment = _mapper.Map<Comment>(newComment);
            await _postRepository.CreatePostCommentAsync(postId, comment);

            var mappedComment = _mapper.Map<CommentGetDto>(comment);

            return CreatedAtAction(nameof(GetPostCommentById), new { postId = postId, commentId = mappedComment }, mappedComment);

        }
    
        [HttpPut]
        [Route("{postId}/comments/{commentId}")]
        public async Task<IActionResult> UpdateCommentRoom(int postId, int commentId, [FromBody] CommentCreateDto updatedComment)
        {
            var toUpdate = _mapper.Map<Comment>(updatedComment);
            toUpdate.CommentId = postId;
            toUpdate.PostId = postId;

            await _postRepository.UpdatePostComment(postId, toUpdate);
            return NoContent();
        }

        [HttpDelete]
        [Route("{postId}/comments/{commentId}")]
        public async Task<IActionResult> RemoveCommentFromPost(int postId, int commentId)
        {
            var comment = await _postRepository.DeletePostCommentAsync(postId, commentId);

            if (comment == null)
            {
                return NotFound("Comment not found");
            }
            return NoContent();
        }

    }
}
