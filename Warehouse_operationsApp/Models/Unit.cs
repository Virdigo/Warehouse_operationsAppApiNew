using System.ComponentModel.DataAnnotations;

namespace Warehouse_operationsApp.Models
{
    public class Unit
    {
        [Key]
        public int id_unit { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
