using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.Models.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = null!;
    }
}
