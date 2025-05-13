using Microsoft.EntityFrameworkCore;
using Warehouse_operationsApp.Data;
using Warehouse_operationsApp.Models;
using Warehouse_operationsApp.Repository.Interfaces;

namespace Warehouse_operationsApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateProduct(int id_product_type, int id_unit, Product Product_create)
        {
            _context.Add(Product_create);
            return Save();
        }

        public bool DeleteProduct(Product Product_delete)
        {
            _context.Remove(Product_delete);
            return Save();
        }

        public ICollection<Product> GetInformation_about_documentsByProduct(int id_inf)
        {
            return _context.Products.Where(r => r.id_Product == id_inf).ToList();
        }

        public Product GetProductById(int ProductId)
        {
            return _context.Products
        .Include(p => p.Product_type)
        .Include(p => p.Unit)
        .FirstOrDefault(r => r.id_Product == ProductId);
        }

        public ICollection<Product> GetProductsList()
        {
            return _context.Products
        .Include(p => p.Product_type)  // Подгружаем тип продукта
        .Include(p => p.Unit)         // Подгружаем единицу измерения
        .ToList();
        }

        public bool ProductExists(int ProductId)
        {
            return _context.Products.Any(r => r.id_Product == ProductId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduct(int id_product_type, int id_unit, Product Product_update)
        {
            _context.Update(Product_update);
            return Save();
        }
    }
}
