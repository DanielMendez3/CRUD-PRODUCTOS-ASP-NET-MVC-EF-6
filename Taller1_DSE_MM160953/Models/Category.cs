using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Taller1_DSE_MM160953.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Categoría")]
        public int IdCategory { get; set; }
        [MaxLength(250)]
        [Required]
        [Display(Name = "Categoria")]
        public string Descripcion { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}