using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommentService.Entities;

namespace CommentService.Services.IService
{
    public interface IComments
    {
        Task<string> CreateComments(Comment CommentsDto);

        Task<Comment> GetComments(string userId);

        Task<string> UpdateComments(Comment CommentsDto);

        Task<string> DeleteComments(Comment Comment);
        Task<List<Comment>> GetPostComments(string postId);
        Task<List<Comment>> GetAllComments();
    }
}
