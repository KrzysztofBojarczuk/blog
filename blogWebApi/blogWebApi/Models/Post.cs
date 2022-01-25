namespace blogWebApi.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public DateTime DateTime { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
