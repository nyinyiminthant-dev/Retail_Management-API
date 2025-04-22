using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTOs
{
    public class ResponseUserLoginDTO
    {
        public int UserId { get; set; }
    
        public string? Email { get; set; }
    
        public bool EmailStatus { get; set; }
        public string? Token { get; set; }
        public bool PasswordStatus { get; set; }
    }
}
