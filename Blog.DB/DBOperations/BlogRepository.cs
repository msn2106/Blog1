using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Model;

namespace Blog.DB.DBOperations
{
    // This class contains all the CRUD features for Post in the Blog
    public class BlogRepository
    {
        BlogEntities db = null;
        public BlogRepository()
        {
            db = new BlogEntities();
        }
        
        // Function to find the post with particular id
        public Post Find(int? id)
        {
            using (var context = new BlogEntities())
            {
                var result = context.Post.Find(id);

                return result;
            }
        }


        // Function to create a post/blog
        public int AddBlog(BlogModel model)
        {
            using (var context = new BlogEntities())
            {
                Post post = new Post()
                {
                    DOP = model.DOP,
                    Title = model.Title,
                    Description = model.Description,
                    Author = model.Author,
                    Likes = model.Likes,
                    Report = model.Report,
                    UserID = model.UserID,
                };

                context.Post.Add(post);

                context.SaveChanges();

                return post.Id;
            }
        }

        // Function to get all posts/blogs
        public List<BlogModel> GetAllBlogs()
        {
            using (var context = new BlogEntities())
            {
                var result = context.Post
                    .Select(x => new BlogModel() 
                    {
                        Title = x.Title,
                        DOP = x.DOP,
                        Author = x.Author,
                        Description = x.Description,
                        Likes = x.Likes,
                        Report = x.Report,
                    }).ToList();

                return result;
            }
        }

        //Function to get a particular post/blog
        public BlogModel GetBlog(string author)
        {
            using (var context = new BlogEntities())
            {
                var result = context.Post
                    .Where(x => x.Author == author)
                    .Select(x => new BlogModel()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        DOP = x.DOP,
                        Description = x.Description,
                        Author = x.Author,
                        Likes = x.Likes,
                        Report = x.Report,
                    }).FirstOrDefault();

                return result;
            }
        }

        // Function to edit a particular blog
        public bool EditBlog(int id, BlogModel model)
        {
            using(var context = new BlogEntities())
            {
                var blog = context.Post.FirstOrDefault(x => x.Id == id);

                if (blog != null)
                {
                    blog.Id = model.Id;
                    blog.Title = model.Title;
                    blog.DOP = model.DOP;
                    blog.Description = model.Description;
                    blog.Author = model.Author;
                }

                context.SaveChanges();
                return true;
            }
        }

        // Function to delete a blog
        public bool DeleteBlog(int id)
        {
            using (var context = new BlogEntities())
            {
                var blog = context.Post.FirstOrDefault(x => x.Id == id);
                if (blog != null)
                {
                    context.Post.Remove(blog);
                    context.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
    }
}
