using Warehouse_operationsApp.Models;

namespace Warehouse_operationsApp.Repository.Interfaces
{
    public interface IProduct_typeRepository
    {
        ICollection<Product_type> GetProductTypesList();
        Product_type GetProductTypeById(int id);
        ICollection<Product> GetProductsByProduct_type(int Id_product_type);
        bool Product_typeExists(int id);
        bool CreateProduct_type(Product_type Product_type_create);
        bool UpdateProduct_type(Product_type Product_type_update);
        bool DeleteProduct_type(Product_type Product_type_delete);
        bool Save();
    }
}
