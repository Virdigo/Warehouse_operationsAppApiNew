using System.ComponentModel.DataAnnotations;

namespace Warehouse_operationsApp.Models
{
    public class Suppliers
    {
        [Key]
        public int id_suppliers { get; set; }
        public string Name { get; set; }
        public string Contact_Information { get; set; }
        public ICollection<Information_about_documents> Information_about_documents { get; set; }
    }
}
