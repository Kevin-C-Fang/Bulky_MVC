using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author {  get; set; }

        [Required]
        [Display(Name  = "List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        // In entity framework, you need a property to represent the foreign key id and a navigation property to the category table.
        // In SQL, you would have a column categoryId and a constraint on foreign key added in SQL server.
        public int CategoryId { get; set; }

        // Explicitly define the Category property annotation so it is used for foreign key navigation for the category ID.
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        // Tells EF core about 1-many relation between product-productImages
        [ValidateNever]
        public List<ProductImage> ProductImages { get; set; }
    }
}
