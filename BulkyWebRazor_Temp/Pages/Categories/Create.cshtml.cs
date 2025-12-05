using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    // [BindProperties] : Annotation that binds all properties if you want other individually binding is fine.
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        // Annotate this so that Category is populated when posting to bind property and is automatically handled so it doesn't need to be passed in.
        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";

            return RedirectToPage("Index");
        }
    }
}
