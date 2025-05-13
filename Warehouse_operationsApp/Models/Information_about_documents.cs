using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Warehouse_operationsApp.Models
{
    public class Information_about_documents
    {
        [Key]
        public int id_inf_doc { get; set; }
        public int id_Product { get; set; }
        public int Quanity { get; set; }
        public int id_doc { get; set; }
        public int id_suppliers { get; set; }
        public int Cost { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Price { get; private set; }
        public Product Product { get; set; }
        public Receipt_and_expense_documents Receipt_and_expense_documents { get; set; }
        public Suppliers Suppliers { get; set; }

    }
}
