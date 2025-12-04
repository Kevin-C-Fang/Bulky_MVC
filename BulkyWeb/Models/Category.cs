using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        // Annotation sets this as primary key
        // You can also use "Id" or "modelName+Id", entity framework automatically assumes this would be the primary key
        [Key]
        public int Id { get; set; }

        // Required annotation sets this as not null setting
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage ="Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }


        /*
         * MaxLength/Range are server-side validations enforced on the property.
         *      As seen above, you can also specify parameters such as error message to change what is shown on the UI side.
         * DisplayName sets the display text if used in a tag helper
         */
    }
}
