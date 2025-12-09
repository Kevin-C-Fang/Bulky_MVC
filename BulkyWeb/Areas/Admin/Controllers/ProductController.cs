using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        // Interface provided by default and gives access to the wwwroot path/root folder
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        #region old code
        /*
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();

                TempData["success"] = "Product created successfully";

                return RedirectToAction("Index");
            }

            return View();
        }
        
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }


        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";

                return RedirectToAction("Index");
            }

            return View();
        }
        */
        #endregion

        // Combines update and insert/Create functionality
        public IActionResult Upsert(int? id)
        {
            // We need to populate categories but we can't pass more than one object. There are 4 routes
            // They all transfer data from controller to view and is useful for when the needed data is not in the model.
            // 1. Viewbag - dynamic property that wraps around ViewData, any # of values canbe assigned, any name can be used as key-value pair, and only laps for current http request.
            // Because Viewbag wraps around/inserts into ViewData, the property name and key of viewdata must not match.
            // Ex. Controller: ViewBag.CategoryList = categoryList; View: asp-items="ViewBag.CategoryList"
            // 2. Viewdata - differs that it's a dictionary type, so value must be type cast before use.
            // Ex. Controller: ViewData["CategoryList"] = categoryList; View: asp-items="@(ViewData["CategoryList"] as IEnumerable<SelectListItem>)"
            // 3. TempData - stores data between 2 consecutive requests. Must be type cast and null checked and only stores one time messages.
            // Ex. Controller: TempData["CategoryList" = categoryList; View: asp-items="@(TempData["CategoryList"] as IEnumerable<SelectListItem>)"

            // 4. Use viewmodel to encapsulate all data needed.

            ProductVM productVM = new()
            {
                // EF core projections allow you to convert a container into another while selectively setting some properties.
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };

            // If id is null, we are displaying whether we are creating a product, else we are updating a product
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u=> u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                // Uses the file passed in to copy and create a new file at "images\product" with a new name.
                // A new image was uploaded.
                if (file != null)   
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    // There was a preexisting file saved, so delete old image.
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                // If post from create, the ID will be 0 and on update it will be filled in.
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                    TempData["success"] = "Product created successfully";
                }
                else{
                    _unitOfWork.Product.Update(productVM.Product);
                    TempData["success"] = "Product updated successfully";
                }

                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            // Exception shown when returning view without parameter due to CategoryList not being populated and passed in.
            // Since we still want the current data and to just populate the category list, use productVM to populate category list
            productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(productVM);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
