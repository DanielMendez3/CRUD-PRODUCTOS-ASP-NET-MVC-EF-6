using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Taller1_DSE_MM160953.Models
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }
        [MaxLength(250)]
        [Required]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        [Required]
        [Display(Name = "Categoría")]
        public int IdCategory { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        [Display(Name = "Costo")]
        public double Cost { get; set; }
        [Required]
        [Display(Name = "Precio")]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Stock")]
        public int Stock { get; set; }
        [Required]
        [Display(Name = "Cantidad de pedidos")]
        public int NumberOfOrders { get; set; }
    }
}