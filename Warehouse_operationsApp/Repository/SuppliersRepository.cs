using AutoMapper;
using Warehouse_operationsApp.Data;
using Warehouse_operationsApp.Models;
using Warehouse_operationsApp.Repository.Interfaces;

namespace Warehouse_operationsApp.Repository
{
    public class SuppliersRepository : ISuppliersRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SuppliersRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ICollection<Information_about_documents> GetInformation_about_documents(int Id_suppliers)
        {
            return _context.Information_About_Documentss.Where(c => c.Suppliers.id_suppliers == Id_suppliers).ToList();
        }

        public ICollection<Suppliers> GetSuppliersList()
        {
            return _context.Supplierss.ToList();
        }

        public Suppliers GetSuppliersById(int id)
        {
            return _context.Supplierss.Where(c => c.id_suppliers == id).FirstOrDefault();
        }

        public Suppliers GetSuppliersByInformation_about_documents(int Id_inf_doc)
        {
            return _context.Information_About_Documentss.Where(o => o.id_inf_doc == Id_inf_doc).Select(c => c.Suppliers).FirstOrDefault();
        }

        public bool SuppliersExists(int id)
        {
            return _context.Supplierss.Any(c => c.id_suppliers == id);
        }

        public bool CreateSuppliers(Suppliers suppliers_create)
        {
            _context.Add(suppliers_create);
            return Save();
        }

        public bool UpdateSuppliers(Suppliers suppliers_update)
        {
            _context.Update(suppliers_update);
            return Save();
        }

        public bool DeleteSuppliers(Suppliers suppliers_delete)
        {
            _context.Remove(suppliers_delete);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
