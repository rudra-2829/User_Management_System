using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarohiWPFCore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
