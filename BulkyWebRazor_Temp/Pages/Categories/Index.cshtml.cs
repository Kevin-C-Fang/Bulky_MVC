using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> CategoryList { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            // I think we could just grab _db and populate on the UI side, but because it's server rendered views and a bad idea to run database queries on the
            // client side everytime.
            CategoryList = _db.Categories.ToList();

            // No need to return view because whatever is set here in the model is easily accessible because it's the model in the razor page.
        }
    }
}
