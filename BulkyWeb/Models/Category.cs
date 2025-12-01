using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        // Annotation sets this as primary key
        // You can also use "Id" or "modelName+Id", entity framework automatically assumes this would be the primary key
        [Key]
        public int Id { get; set; }

        // Annotation sets this as not null setting
        [Required]
        public string Name { get; set; }


        public int DisplayOrder { get; set; }
    }
}
