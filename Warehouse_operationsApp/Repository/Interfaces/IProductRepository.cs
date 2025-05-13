using Warehouse_operationsApp.Models;

namespace Warehouse_operationsApp.Repository.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProductsList();
        Product GetProductById(int ProductId);
        ICollection<Product> GetInformation_about_documentsByProduct(int id_inf);
        bool ProductExists(int ProductId);
        bool CreateProduct(int id_product_type, int id_unit, Product Product_create);
        bool UpdateProduct(int id_product_type, int id_unit, Product Product_update);
        bool DeleteProduct(Product Product_delete);
        bool Save();
    }
}
