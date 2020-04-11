using Liments.MVC.Core.Entities;
using System.Collections.Generic;

namespace Liments.MVC.Models
{
    public class PostViewModel
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public string PostedAt { get; set; }
    }
}
