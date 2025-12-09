using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.ViewModels
{
    // Due to the view being tightly bounded to the model and the possibility of needing multiple data, you can create a viewmodel which is a combination of objects.
    // This way, you can bind the viewmodel to the view and it can access the data through the container.
    public class ProductVM
    {
        public Product Product { get; set; }

        // Ensures ModelState doesn't check for validation on this property because we don't want to instantiate this property when on httppost
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
