using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();

            return View(objCategoryList);
        }

        public IActionResult Create() 
        {
            // We use a model to create a category inside the view, but we don't need to pass a new Category since if nothing is passed, it will create a new object
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            /* Example custom validation specific to property, so that name != DisplayOrder.
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                // AddModelError: 1st parameter is what you would use asp-for so that would be where the error would show.
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");

            }*/

            /* Example custom validation specific to model, NOT property
            if (obj?.Name?.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value.");
            }
            */

            // Checks if the category obj is valid, it will go to category class and examine validation rules and ensure all rules are followed for modelstate to be valid.
            if (ModelState.IsValid)
            {
                // DB Context/entity framework core provides a method Add to log the change but you also have to SaveChanges() to apply to database.
                _db.Categories.Add(obj);
                _db.SaveChanges();

                // Tempdata is used to display something for only the next rendering such as notifications
                TempData["success"] = "Category created successfully";

                // Redirects to category index view, if in same controller you can just write the action name, but if going to a different controller,
                // You write the controller name: RedirectToAction("Index", "Category");
                return RedirectToAction("Index");
            }

            // Since validation failed, we return to the Create view and in there you can use span and tag helpers to display the error messages for failed validations.
            // Not sure how failed validation is passed into view, but I assume somehow the modelstate is passed in and with tag helpers it generates the error message
            return View();
        }

        // By default, if not defined it is a get action method
        // Use asp-route-id="@obj.Id" to pass property in. The name where "id" is should either map to the get action parameter or the model property name.
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            // Different methods of getting specific category.
            // Find and FirstOrDefault may seem similar, but the operation is different in entity framework.
            // Find looks in the memory db context without looking in database, FirstOrDefault looks in database
            // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);          
            // Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();  Allows for more filtering

            // Find only works on primary key
            Category? categoryFromDb = _db.Categories.Find(id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }

            // When passing in category it autopopulates the input fields using the asp-for tag helper.
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // Because the parameter/action name match the get action method, we define the action name as Delete, but annotate it so it knows it is a post action method.
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
