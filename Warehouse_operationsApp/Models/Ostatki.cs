using System.ComponentModel.DataAnnotations;

namespace Warehouse_operationsApp.Models
{
    public class Ostatki
    {
        [Key]
        public int id_Ostatki { get; set; }
        public int id_warehouses { get; set; }
        public int id_Product { get; set; }
        public int Quantity_Ostatki { get; set; }
        public Product Product { get; set; }
        public Warehouses Warehouses { get; set; }
    }
}
