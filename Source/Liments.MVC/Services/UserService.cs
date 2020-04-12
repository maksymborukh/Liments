using AutoMapper;
using Liments.MVC.Core.Database;
using Liments.MVC.Core.Entities;
using Liments.MVC.Interfaces;
using Liments.MVC.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Liments.MVC.Services
{
    public class UserService : IUserService
    {
        private ILimentsContext _context;
        private IMapper _mapper;

        public UserService(ILimentsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var result = _mapper.Map<IEnumerable<UserViewModel>>(await _context.Users.AsQueryable().ToListAsync());
            return result;
        }

        public async Task<UserViewModel> GetByEmailAsync(string str)
        {
            var result = _mapper.Map<UserViewModel>(await _context.Users.Find(u => u.Email == str).SingleOrDefaultAsync());
            return result;
        }

        public async Task<UserViewModel> GetByIdAsync(string id)
        {
            var result = _mapper.Map<UserViewModel>(await _context.Users.Find(u => u.Id == id).SingleOrDefaultAsync());
            return result;
        }

        public UserViewModel GetByEmail(string str)
        {
            var result = _mapper.Map<UserViewModel>(_context.Users.Find(u => u.Email == str).SingleOrDefault());
            return result;
        }

        public async Task<UserViewModel> GetByUserNameAsync(string str)
        {
            var result = _mapper.Map<UserViewModel>(await _context.Users.Find(u => u.UserName == str).SingleOrDefaultAsync());
            return result;
        }

        public UserViewModel GetByUserName(string str)
        {
            var result = _mapper.Map<UserViewModel>(_context.Users.Find(u => u.UserName == str).SingleOrDefault());
            return result;
        }

        public async Task<bool> CheckCredentials(string name, string pass)
        {
            UserViewModel user = await GetByUserNameAsync(name);

            if (user != null && user.Password == GetHashCode(pass))
                return true;

            return false;
        }

        public async Task<bool> IsUserExist(string email, string name)
        {
            var user1 = await GetByEmailAsync(email);
            var user2 = await GetByUserNameAsync(name);

            if (user1 == null && user2 == null)
                return false;

            return true;
        }

        public async Task CreateAsync(UserViewModel user)
        {
            var nUser = _mapper.Map<User>(user);
            nUser.Password = GetHashCode(nUser.Password);
            await _context.Users.InsertOneAsync(nUser);
        }

        private async Task<IEnumerable<Post>> GetAllLikePostAsync(string userName)
        {
            var like = new Like();
            like.UserName = userName;
            var result = await _context.Posts.Find(p => p.Likes.Contains(like)).ToListAsync();

            return result;
        }

        private async Task<IEnumerable<Post>> GetAllCommentPostAsync(string userName)
        {
            var posts = await _context.Posts.AsQueryable().ToListAsync();

            var result = new List<Post>();
            foreach (var el in posts)
            {
                if (el.Comments.Find(t => t.Author == userName) != null)
                {
                    result.Add(el);
                }
            }           

            return result;
        }

        private async Task<IEnumerable<User>> GetAllFolUserAsync(string userName)
        {
            var result = await _context.Users.Find(u => u.Fol.Contains(userName)).ToListAsync();

            return result;
        }

        private async Task<IEnumerable<User>> GetAllSubUserAsync(string userName)
        {
            var result = await _context.Users.Find(u => u.Subs.Contains(userName)).ToListAsync();

            return result;
        }

        public async Task UpdateAsync(string id, string fname, string lname, string email, string username, string pass)
        {
            var user = _mapper.Map<User>(await GetByIdAsync(id));

            string originUsername = user.UserName;

            if (fname != null)
            {
                user.FirstName = fname;
            }

            if (lname != null)
            {
                user.LastName = lname;
            }

            if (user.Email != email && email != null)
            {
                var uE = await GetByEmailAsync(email);
                if (uE == null)
                {
                    user.Email = email;
                }
                else
                {
                    throw new Exception("Email exists");
                }
            }

            if (user.UserName != username && username != null)
            {
                var uN = await GetByUserNameAsync(username);
                if (uN == null)
                {
                    user.UserName = username;
                }
                else
                {
                    throw new Exception("Username exists");
                }
            }

            if (pass != null)
            {
                user.Password = GetHashCode(pass);
            }

            await _context.Users.ReplaceOneAsync(u => u.Id == user.Id, user);

            if (originUsername != username && username != null)
            {
                await UpdateFolAsync(originUsername, username);
                await UpdateSubAsync(originUsername, username);
                await UpdatePostAuthorAsync(originUsername, username);        
                await UpdatePostLikeAsync(originUsername, username);
                await UpdatePostCommentAsync(originUsername, username);
            }
        }

        public async Task UpdatePostAuthorAsync(string originUsername, string username)
        {
            var posts = await _context.Posts.Find(p => p.Author == originUsername).ToListAsync();

            foreach (var el in posts)
            {
                el.Author = username;
                await _context.Posts.ReplaceOneAsync(p => p.Id == el.Id, el);
            }
        }

        public async Task UpdatePostCommentAsync(string originUsername, string username)
        {
            var posts = await GetAllCommentPostAsync(originUsername);

            foreach (var el in posts)
            {
                var comment = el.Comments.Find(t => t.Author == originUsername);
                el.Comments.Remove(comment);
                comment.Author = username;
                el.Comments.Add(comment);
                await _context.Posts.ReplaceOneAsync(p => p.Id == el.Id, el);
            }
        }

        public async Task UpdatePostLikeAsync(string originUsername, string username)
        {
            var posts = await GetAllLikePostAsync(originUsername);

            foreach (var el in posts)
            {
                var like = el.Likes.Find(t => t.UserName == originUsername);
                el.Likes.Remove(like);
                like.UserName = username;
                el.Likes.Add(like);
                await _context.Posts.ReplaceOneAsync(p => p.Id == el.Id, el);
            }
        }

        public async Task UpdateFolAsync(string originUsername, string username)
        {
            var usersF = await GetAllFolUserAsync(originUsername);

            foreach (var el in usersF)
            {
                el.Fol.Remove(originUsername);
                el.Fol.Add(username);
                await _context.Users.ReplaceOneAsync(u => u.Id == el.Id, el);
            }
        }

        public async Task UpdateSubAsync(string originUsername, string username)
        {
            var usersS = await GetAllSubUserAsync(originUsername);

            foreach (var el in usersS)
            {
                el.Subs.Remove(originUsername);
                el.Subs.Add(username);
                await _context.Users.ReplaceOneAsync(u => u.Id == el.Id, el);
            }
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Users.DeleteOneAsync(u => u.Id == id);
        }

        private string GetHashCode(string pass)
        {
            var hasher = MD5.Create();
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(pass));
            var output = string.Empty;
            foreach (var b in hash)
            {
                output += b.ToString("X2");
            }

            return output;
        }

        public async Task FollowAsync(string userName, string FolUserName)
        {
            var filter = Builders<User>.Filter.Eq(el => el.UserName, userName);
            var update = Builders<User>.Update
                    .Push<string>(el => el.Fol, FolUserName);

            await _context.Users.FindOneAndUpdateAsync(filter, update);

            var filter2 = Builders<User>.Filter.Eq(el => el.UserName, FolUserName);
            var update2 = Builders<User>.Update
                    .Push<string>(el => el.Subs, userName);

            await _context.Users.FindOneAndUpdateAsync(filter2, update2);
        }
        public async Task UnfollowAsync(string userName, string FolUserName)
        {
            var filter = Builders<User>.Filter.Eq(el => el.UserName, userName);
            var update = Builders<User>.Update
                    .Pull<string>(el => el.Fol, FolUserName);

            await _context.Users.FindOneAndUpdateAsync(filter, update);

            var filter2 = Builders<User>.Filter.Eq(el => el.UserName, FolUserName);
            var update2 = Builders<User>.Update
                    .Pull<string>(el => el.Subs, userName);

            await _context.Users.FindOneAndUpdateAsync(filter2, update2);
        }
    }
}
