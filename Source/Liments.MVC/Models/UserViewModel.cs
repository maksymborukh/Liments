using System.Collections.Generic;

namespace Liments.MVC.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<string> AccSubs { get; set; }
        public List<string> PndSubs { get; set; }
        public List<string> AccFol { get; set; }
        public List<string> PndFol { get; set; }
        public string Password { get; set; }
    }
}
