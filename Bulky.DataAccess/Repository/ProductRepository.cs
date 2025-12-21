using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            // Custom update to ensure that image url is updated only when a new one is saved.
            // EF core handles this but being explict prevents ImageUrl from being assigned null.
            var objFromDb = _db.Products.FirstOrDefault(u=>u.Id == obj.Id);

            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Author = obj.Author;

                // You could do this and EF core automatically configures ands saves the product images to its table as well
                // I prefer to explicitly add it rather than implicitly rely on EF core here, because I'm not that clear on the process and just prefer clarity.
                // objFromDb.ProductImages = obj.ProductImages;

                //if (obj.ImageUrl != null)
                //{
                //    objFromDb.ImageUrl = obj.ImageUrl;
                //}
            }

            // By grabbing the object from the DB, changes made are automatically tracked, so no need to explicitly call Update.
            // _db.Products.Update(obj);
        }
    }
}
