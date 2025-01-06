using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blogger.Data;
using Blogger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Blogger.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext _CONTEXT)
        {
            _context = _CONTEXT;
        }
        [Route("/Category/Lists")]
        public IActionResult List()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var category = new Category() { ParentId = 1 };
            return View(category);
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(List));
            }
            else
            {
                ModelState.AddModelError("", "Check the Required Fields!");
                return View(category);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
                return NotFound("Invalid ID");
            var model = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                return View(model);
            }
            return NotFound("Category NOT FOUND!");
        }
        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            if (id <= 0)
                return NotFound("Invalid ID");
            if (ModelState.IsValid)
            {
                var ExistingModel = _context.Categories.Find(id);
                if (ExistingModel != null)
                {
                    ExistingModel.ParentId = category.ParentId;
                    ExistingModel.Slug = category.Slug;
                    ExistingModel.Content = category.Content;
                    ExistingModel.Title = category.Title;
                    ExistingModel.MetaTitle = category.MetaTitle;
                    _context.SaveChanges(true);
                }
                return RedirectToAction(nameof(List));
            }
            else
            {
                ModelState.AddModelError("", "Check Required Fields!");
                return View(category);
            }
        }
        public IActionResult Details([FromRoute] int id)
        {
            if(id <= 0)
                return BadRequest("Invalid ID");
            var model = _context.Categories.FirstOrDefault(x => x.Id == id);
            if(model != null)
                return View(model);
            return NotFound("Categories Not Found!");
        }
        [HttpPost]
        public IActionResult Delete([FromRoute] int id)
        {
            if(id <= 0)
                return NotFound("Invalid Id");
            var model = _context.Categories.FirstOrDefault(x => x.Id == id);
            if(model != null)
            {
                _context.Categories.Remove(model);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(List));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}