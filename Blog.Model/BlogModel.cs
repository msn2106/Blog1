using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Blog.Model
{
    public class BlogModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string DOP { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(500,ErrorMessage ="Length of Description must be less than 500 words.")]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        public int? Likes { get; set; }
        public int? Report { get; set; }
        public int? UserID { get; set; }
        public int? DummyInt { get; set; }
        public string DummyString { get; set; }
        
        public virtual UserModel user { get; set; }

    }
}
