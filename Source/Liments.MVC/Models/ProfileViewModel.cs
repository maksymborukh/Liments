using System.Collections.Generic;

namespace Liments.MVC.Models
{
    public class ProfileViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
        public UserViewModel Profile { get; set; }
    }
}
