using System.ComponentModel.DataAnnotations;

namespace Warehouse_operationsApp.Models
{
    public class Warehouses
    {
        [Key]
        public int id_warehouses { get; set; }
        public string Name { get; set; }
        public string address { get; set; }
        public int id_users { get; set; }
        public ICollection<Ostatki> Ostatki { get; set; }
        public Users Users { get; set; }
    }
}
