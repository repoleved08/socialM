using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using CommentService.Entities;
using CommentService.Entities.Dtos;

namespace CommentService.Profiles
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<Comment, CommentsDto>().ReverseMap();
            CreateMap<Post, PostCommentDto>().ReverseMap();
        }
    }
}
