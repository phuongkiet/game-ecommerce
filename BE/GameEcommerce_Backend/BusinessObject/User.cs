using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class User : IdentityUser<int>
    {
        public string? Name { get; set; }
        public byte Status {  get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }

    }
}
