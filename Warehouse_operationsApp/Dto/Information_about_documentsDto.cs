using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse_operationsApp.Dto
{
    public class Information_about_documentsDto
    {
        public int id_inf_doc { get; set; }
        public int id_Product { get; set; }
        public int Quanity { get; set; }
        public int id_doc { get; set; }
        public int id_suppliers { get; set; }
        public int Cost { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Price { get; private set; }
    }
}
