using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebView.Models
{
    public class AppUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }
    }

    public class Role
    {
        public string RoleName { get; set; }
    }
}
