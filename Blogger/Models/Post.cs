using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blogger.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace Blogger.Models
{
    public class Post
    {
        [Key]
        [BindNever]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Title must be between 5 and 50 characters.", MinimumLength = 5)]
        [Required(ErrorMessage = "Title is required.")]
        [Display(Name = "Enter The Blog Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [DisplayName("Blog Content")]
        public string Content { get; set; } 

        [MaxLength(50, ErrorMessage = "Slug must be below 50 characters.")]
        [Display(Name = "Slug of The Blog")]
        public string Slug { get; set; } 

        [BindNever]
        public DateTime CreatedAt { get; set; }
        [BindNever]
        public DateTime UpdatedAt { get; set; }
        [BindNever]
        public DateTime PublishedAt { get; set; }
        [BindNever]
        public int CategoryId { get; set; }
        [BindNever]
        public int UserId { get; set; }

        [MaxLength(50, ErrorMessage = "URL must be below 50 characters.")]
        public string Media { get; set; } 

        [DisplayName("Provide Blog Status")]
        public Status Status { get; set; }

        public Post()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            PublishedAt = DateTime.UtcNow;
            Status = Status.Default;
            CategoryId = 1;
            UserId = 1;
            Slug = Guid.NewGuid().ToString();
            Media = "/images/default-post.jpg";
        }
    }
}

