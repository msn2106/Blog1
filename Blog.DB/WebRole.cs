using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DB
{
    public class WebRole
    {
        // This function is used to fetch role based on username given. It joins the two tables User and UserRole and fetches role corresponding to userID which in turn is linked with ID in user table
        public static string[] RoleProvider(string username)
        {
            using(var context = new BlogEntities())
            {
                var result = (from user in context.User
                              join role in context.UserRole on user.Id equals role.UserId
                              where user.UserName == username
                              select role.Role).ToArray();

                return result;
            }
        }
    }
}
