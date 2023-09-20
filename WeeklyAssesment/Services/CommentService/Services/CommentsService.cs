using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommentService.Data;
using CommentService.Entities;
using CommentService.Services.IService;

namespace CommentsService.Services
{
    public class CommentsService : IComments
    {
        private readonly CommentsDbContext _commentsDbContext;
        private readonly IMapper _mapper;

        public CommentsService(CommentsDbContext commentsDbContext, IMapper mapper)
        {
            _commentsDbContext = commentsDbContext;
            _mapper = mapper;
        }

        public Task<string> CreateComments(Comment CommentsDto)
        {
            var comm = _commentsDbContext.Comments.
        }

        public Task<string> DeleteComments(Comment Comment)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllComments()
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetComments(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetPostComments(string postId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateComments(Comment CommentsDto)
        {
            throw new NotImplementedException();
        }
    }
}
