using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Warehouse_operationsApp.Models
{
    public class Receipt_and_expense_documents
    {
        [Key]
        public int id_doc { get; set; }
        public DateTime date { get; set; }
        public bool ReceiptAndexpense_documents { get; set; }
        public int id_users { get; set; }
        public ICollection<Information_about_documents> Information_about_documents { get; set; }
        public Users Users { get; set; }
    }
}
