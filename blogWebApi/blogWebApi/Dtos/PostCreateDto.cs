using blogWebApi.Models;

namespace blogWebApi.Dtos
{
    public class PostCreateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
    }
}
