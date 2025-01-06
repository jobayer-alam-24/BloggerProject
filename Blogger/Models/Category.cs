using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blogger.Models
{
    public class Category
    {
        [Key]
        [BindNever]
        public int Id {get; set;}
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Parent ID MUST BE Positive!")]
        public int ParentId {get; set;}
        [Required]
        [StringLength(100, ErrorMessage = "Title can not be longer than 100 characters!")]
        public string Title {get; set;}
        [StringLength(100, ErrorMessage = "Meta Title Can not be longer than 100 characters")]
        [DisplayName("Meta Title")]
        public string MetaTitle {get; set;}
        [Required(ErrorMessage = "SLUG can not be NULL OR EMPTY")]
        [StringLength(50, ErrorMessage = "SLUG can not be longer than 50 characters!")]
        [DisplayName("URL Slug")]
        public string Slug {get; set;}
        [Required]
        [MinLength(10, ErrorMessage = "Content must be above 10 characters!")]
        public string Content {get; set;}
    }
}