using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService.Data;
using EmailService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailService.Services
{
    public class EmailService
    {
        private DbContextOptions<EmailDbContext> options;

        public EmailService()
        {
        }

        public EmailService(DbContextOptions<EmailDbContext> options)
        {
            this.options = options;
        }


        public async Task SaveData(EmailLoggers emailLoggers)
        {
            //create _context

            var _context = new EmailDbContext(this.options);
            _context.EmailLoggers.Add(emailLoggers);
            await _context.SaveChangesAsync();
        }

    }
}
