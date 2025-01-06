using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AspNetCoreGeneratedDocument;
using Blogger.Data;
using Blogger.Enums;
using Blogger.Models;
using Blogger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blogger.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PostController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        // private List<Post> _posts = PostService.GetAllPosts();

        //Search logic
        public IActionResult List([FromQuery(Name = "search")] string search_key, Status? status)
        {
            List<Post> posts = new List<Post>();
            if (string.IsNullOrEmpty(search_key) && status == null)
            {
                posts = _context.Posts.ToList();
                // posts.AddRange(_posts);
                return View(posts);
            }
            else if (status == null && !string.IsNullOrEmpty(search_key))
            {
                var post = _context.Posts.Where(x => x.Content.ToLower().Contains(search_key.ToLower())).ToList();
                return View(post);
            }
            else if (status != null && string.IsNullOrEmpty(search_key))
            {
                var post = _context.Posts.Where(x => x.Status == status).ToList();
                return View(post);
            }
            else
            {
                var post = _context.Posts.Where(x => x.Content.ToLower().Contains(search_key.ToLower()) && x.Status == status).ToList();
                return View(post);
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Posts.Add(post);
                _context.SaveChanges();
                TempData["success-messege"] = "Post created successfully!";
                return RedirectToAction("List");
            }
            ModelState.AddModelError(" ", "Check Required Fields!");
            return View(post);
        }
        public IActionResult Edit(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");
            var post = _context.Posts.FirstOrDefault(x => x.Id == id);
            if (post == null)
                return NotFound("Post Not Found!");
            return View(post);
        }
        [HttpPost]
        public IActionResult Edit(int id, Post post)
        {
            if (ModelState.IsValid)
            {
                var ExistingPosts = _context.Posts.FirstOrDefault(x => x.Id == id);
                ExistingPosts.CategoryId = post.CategoryId;
                ExistingPosts.Content = post.Content;
                ExistingPosts.CreatedAt = post.CreatedAt;
                ExistingPosts.PublishedAt = post.PublishedAt;
                ExistingPosts.UpdatedAt = post.UpdatedAt;
                ExistingPosts.Slug = post.Slug;
                ExistingPosts.Media = post.Media;
                ExistingPosts.Status = post.Status;
                ExistingPosts.UserId = post.UserId;
                ExistingPosts.Title = post.Title;
                _context.SaveChanges(true);
                TempData["success-messege"] = "Post Updated Successfully!";
                return RedirectToAction(nameof(List));
            }
            ModelState.AddModelError(" ", "Check Required Fields!");
            return View(post);
        }
        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");
            var model = _context.Posts.FirstOrDefault(x => x.Id == id);
            if (model != null)
                return View(model);
            return NotFound("Post Not Found!");
        }
        [HttpPost]
        public IActionResult Delete([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");
            var model = _context.Posts.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return NotFound("Post Not Found!");

            _context.Posts.Remove(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}