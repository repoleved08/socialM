using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommentService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Data
{
    public class CommentsDbContext : DbContext
    {
        public CommentsDbContext(DbContextOptions<CommentsDbContext> options) : base(options) { }

        public DbSet<Comment> Comments { get; set; }
    }
}
