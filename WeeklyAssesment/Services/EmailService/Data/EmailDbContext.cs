using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailService.Data
{
    public class EmailDbContext : DbContext
    {
        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options) { }
        public DbSet<EmailLoggers> EmailLoggers { get; set; }
    }
}
