using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebView.Models;

namespace WebView.Repositories
{
    public class UserRepository
    {
        private static List<AppUser> Users = new List<AppUser>
        {
            new AppUser{UserName="adam",Password="password" },
            new AppUser{ UserName="bat",Password="password" }
        };
        public static AppUser GetUser(string userName,string password)
        {
            AppUser user = Users.Where(t => t.UserName == userName && t.Password == password).FirstOrDefault();
            user.Roles = new List<Role>();
            user.Roles.Add(new Role { RoleName = "admin" });
            return user;
        }
    }
}
