using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse_operationsApp.Models
{
    public class Users
    {
        [Key]
        public int id_users { get; set; }
        public string FIO { get; set; }
        public string Login { get; set; }
        public string password { get; set; }

        [ForeignKey("Doljnosti")]
        public int id_doljnosti { get; set; }
        public Doljnosti Doljnosti { get; set; }

        public ICollection<Receipt_and_expense_documents> Receipt_and_expense_documents { get; set; }
        public ICollection<Warehouses> Warehouses { get; set; }
    }
}
