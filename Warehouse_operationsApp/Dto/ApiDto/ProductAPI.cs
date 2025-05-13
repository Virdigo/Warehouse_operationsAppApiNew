namespace Warehouse_operationsApp.Dto.ApiDto
{
    public class ProductAPI
    {
        public int id_Product { get; set; }
        public string Name { get; set; }
        public string vendor_code { get; set; }
        public int Price { get; set; }

        public string ProductTypeName { get; set; } // Вместо id_product_type
        public string UnitName { get; set; }       // Вместо id_unit
    }
}
