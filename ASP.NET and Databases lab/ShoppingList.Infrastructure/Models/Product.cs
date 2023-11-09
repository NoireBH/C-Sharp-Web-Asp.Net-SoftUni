using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Infrastructure.Models
{
    public class Product
    {
        public Product()
        {
            ProductNotes = new HashSet<ProductNote>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = null!;

        [Required]
        public ICollection<ProductNote> ProductNotes { get; set; }
    }
}
