﻿using AutoMapper;
using Liments.MVC.Core.Database;
using Liments.MVC.Core.Entities;
using Liments.MVC.Interfaces;
using Liments.MVC.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liments.MVC.Services
{
    public class PostService : IPostService
    {
        private ILimentsContext _context;
        private IMapper _mapper;
        private IUserService _userService;

        public PostService(ILimentsContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<PostViewModel> GetByIdAsync(string postId)
        {
            var entity = await _context.Posts.Find(p => p.Id == postId).SingleAsync();
            return _mapper.Map<PostViewModel>(entity);
        }

        public async Task<IEnumerable<PostViewModel>> GetAllPublicAsync()
        {
            var result = _mapper.Map<IEnumerable<PostViewModel>>(await _context.Posts.Find(p => p.Access.Public == true).ToListAsync());
            return result;
        }

        public async Task<PostViewModel> GetByAuthorAsync(string str)
        {
            var result = _mapper.Map<PostViewModel>(await _context.Posts.Find(p => p.Author == str).SingleOrDefaultAsync());
            return result;
        }

        public async Task CreateAsync(PostViewModel post)
        {
            var nPost = _mapper.Map<Post>(post);
            await _context.Posts.InsertOneAsync(nPost);
        }

        public async Task UpdateAsync(PostViewModel post)
        {
            var nPost = _mapper.Map<Post>(post);
            await _context.Posts.ReplaceOneAsync(p => p.Id == nPost.Id, nPost);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Posts.DeleteOneAsync(p => p.Id == id);
        }
        
        public void Like(string userName, string postId)
        {
            if (IsLiked(userName, postId))
            {
                RemoveLike(userName, postId);
            }
            else
            {
                AddLike(userName, postId);
            }
        }

        public bool IsLiked(string userName, string postId)
        {
            var post = _mapper.Map<PostViewModel>(_context.Posts.Find(p => p.Id == postId).Single());
            return post.Likes.AsEnumerable().Any(p => p.UserName == userName);
        }

        private void AddLike(string userName, string postId)
        {
            var like = _mapper.Map<Like>(_userService.GetByUserName(userName));

            var filter = Builders<Post>.Filter.Eq(el => el.Id, postId);
            var update = Builders<Post>.Update
                    .Push<Like>(el => el.Likes, like);

             _context.Posts.FindOneAndUpdate(filter, update);
        }
        private void RemoveLike(string userName, string postId)
        {
            var like = _mapper.Map<Like>(_userService.GetByUserName(userName));

            var filter = Builders<Post>.Filter.Eq(el => el.Id, postId);
            var update = Builders<Post>.Update
                    .Pull<Like>(el => el.Likes, like);

             _context.Posts.FindOneAndUpdate(filter, update);
        }
    }
}
