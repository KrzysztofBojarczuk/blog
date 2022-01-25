using blogWebApi.Models;

namespace blogWebApi.Dtos
{
    public class PostGetDto
    {
        public int PostId { get; set; }
        public DateTime DateTime { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
