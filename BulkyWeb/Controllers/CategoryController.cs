using Bulky.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bulky.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) {
            _db = db;
        
        }
        public IActionResult Index()
        {
            List<Category> categoryObj = _db.Categories.ToList();  
            return View(categoryObj);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //custom validation for the input field 
            // Type of the summery is All, ModelOnly, None
            //if(obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "Name can't be same as display order");
            //}
            //string input = obj.Name;

            //if (int.TryParse(input, out _))
            //{
            //    ModelState.AddModelError("name", "Name can't be a number");
            //}

                if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                TempData["success"] = "Category created successfully";
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View();
          
           
        }


        public IActionResult Edit(int? Id)
        {
            if(Id==null || Id==0)
            {
                return NotFound();
            }
            Category ? categoriesdb = _db.Categories.Find(Id);
            //Category ? categoriesdb1 = _db.Categories.FirstOrDefault(u=>u.Id==Id);
            //Category ? categoriesdb2 = _db.Categories.First(u=>u.Id==Id);
            //Category ? categoriesdb3 = _db.Categories.Where(u=>u.Id==Id).FirstOrDefault();
            if (categoriesdb == null )
            {
                return NotFound();
            }
            return View(categoriesdb);
        }


        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);

                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();


        }
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category? categoriesdb = _db.Categories.Find(Id);
            //Category ? categoriesdb1 = _db.Categories.FirstOrDefault(u=>u.Id==Id);
            //Category ? categoriesdb2 = _db.Categories.First(u=>u.Id==Id);
            //Category ? categoriesdb3 = _db.Categories.Where(u=>u.Id==Id).FirstOrDefault();
            if (categoriesdb == null)
            {
                return NotFound();
            }
            return View(categoriesdb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Category? obj = _db.Categories.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();


        }
    }
}
