using Liments.MVC.Core.Entities;
using Liments.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liments.MVC.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostViewModel>> GetAllPublicAsync();
        Task<PostViewModel> GetByIdAsync(string postId);
        Task<PostViewModel> GetByAuthorAsync(string str);
        Task CreateAsync(PostViewModel item);
        Task UpdateAsync(PostViewModel item);
        Task DeleteAsync(string id);
        void Like(string userName, string postId);
        bool IsLiked(string userName, string postId);

    }
}
