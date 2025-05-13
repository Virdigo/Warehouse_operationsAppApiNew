using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace Warehouse_operationsApp.Models
{
    public class Product
    {
        [Key]
        public int id_Product { get; set; }
        public string Name { get; set; }
        public string vendor_code { get; set; }
        public int Price { get; set; }
        public int id_product_type { get; set; }
        public int id_unit { get; set; }
        public Information_about_documents Information_about_documents { get; set; }
        public ICollection<Ostatki> Ostatki { get; set; }
        public Product_type Product_type { get; set; }
        public Unit Unit { get; set; }
    }
}
