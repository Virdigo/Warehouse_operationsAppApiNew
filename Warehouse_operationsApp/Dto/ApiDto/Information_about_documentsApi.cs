using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse_operationsApp.Dto.ApiDto
{
    public class Information_about_documentsApi
    {
        public int id_inf_doc { get; set; }
        public string ProductName { get; set; }
        public int Quanity { get; set; }
        public string Receipt_and_expense_documentsName { get; set; }
        public string SuppliersName { get; set; }
        public int Cost { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Price { get; private set; }
    }
}
