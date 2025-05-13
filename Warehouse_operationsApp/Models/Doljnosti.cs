using System.ComponentModel.DataAnnotations;

namespace Warehouse_operationsApp.Models
{
    public class Doljnosti
    {
        [Key]
        public int id_doljnosti { get; set; }
        public string Post { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
