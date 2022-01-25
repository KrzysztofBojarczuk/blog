using blogWebApi.Models;

namespace blogWebApi.Dtos
{
    public class CommentGetDto
    {
        public int CommentId { get; set; }
        public DateTime DateTime { get; set; }
        public string Commented { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
