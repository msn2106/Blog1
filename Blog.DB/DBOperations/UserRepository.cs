using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Model;

namespace Blog.DB.DBOperations
{
    // This class contains all the user operations linked with Database - CRUD + 
    public class UserRepository
    {
        // Function to Add new User
        public int AddUser(UserModel model)
        {
            using(var context = new BlogEntities())
            {
                User user = new User()
                {
                    Name = model.Name,
                    Email = model.Email,
                    UserName = model.UserName,
                    Password = model.Password
                };

                context.User.Add(user);

                context.SaveChanges();

                return user.Id;

            }
        }

        // Function to view all the Users
        public List<UserModel> GetAllUsers()
        {
            using(var context = new BlogEntities())
            {
                var result = context.User
                    .Select(x => new UserModel() 
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        UserName = x.UserName,
                        Password = x.Password
                    }).ToList();

                return result;
            }
        }

        // Get Details of a particular user based on ID
        public UserModel GetUser(int id)
        {
            using(var context = new BlogEntities())
            {
                var result = context.User
                    .Where(x => x.Id == id)
                    .Select(x => new UserModel() 
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        UserName = x.UserName,
                        Password = x.Password
                    }).FirstOrDefault();

                return result;
            }
        }

        // Function to edit the record of any user
        public bool EditDetails(int id, UserModel model)
        {
           using(var context = new BlogEntities())
           {
                var user = context.User.FirstOrDefault(x => x.Id == id);

                if(user != null)
                {
                    user.Id = model.Id;
                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.Password = model.Password;
                }

                context.SaveChanges();
                return true;

           }
        }

        // Function to delete user - issue here - now working - In controller Find method replaced with GetUser and in below method simultaneous delete using foreign key is implemented
        public bool DeleteUser(int id)
        {
            using(var context = new BlogEntities())
            {
                var user = context.User.FirstOrDefault(x => x.Id == id);
                if(user != null)
                {
                    var inUserRole = context.UserRole.Where(x => x.UserId == id);
                    
                    foreach(var item in inUserRole)
                    {
                        context.UserRole.Remove(item);
                    }
                    context.User.Remove(user);
                    context.SaveChanges();
                    return true;
                }
                else return false;
            }
        }

        // Function for login feature
        public bool Login(Membership model)
        {
            using(var context = new BlogEntities())
            {
                bool isValid = context.User.Any(x => x.UserName == model.UserName && x.Password == model.Password);
                return isValid;
            }
        }

        //Function to find a user 
        public User Find(int id)
        {
            using(var context = new BlogEntities())
            {
                var result = context.User.Find(id);

                return result;
            }
        }
    }
}
