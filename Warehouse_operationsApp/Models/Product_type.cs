using System.ComponentModel.DataAnnotations;

namespace Warehouse_operationsApp.Models
{
    public class Product_type
    {
        [Key]
        public int id_product_type { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
