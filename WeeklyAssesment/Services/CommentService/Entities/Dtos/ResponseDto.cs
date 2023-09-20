using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Entities.Dtos
{
    public class ResponseDto
    {
        public object? Data { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
    }
}
