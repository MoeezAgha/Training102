using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training102.BAL.SharedModel
{
    public class UserManagerResponse
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        // You can include additional data properties here if needed
        // For example, to return user information after registration or login
        public string? UserId { get; set; }
        public string? Username { get; set; }
    }
}
