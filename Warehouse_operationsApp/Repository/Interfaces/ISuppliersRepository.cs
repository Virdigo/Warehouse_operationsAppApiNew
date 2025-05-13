using Warehouse_operationsApp.Models;

namespace Warehouse_operationsApp.Repository.Interfaces
{
    public interface ISuppliersRepository
    {
        ICollection<Suppliers> GetSuppliersList();
        Suppliers GetSuppliersById(int id);
        Suppliers GetSuppliersByInformation_about_documents(int Id_inf_doc);
        ICollection<Information_about_documents> GetInformation_about_documents(int id_suppliers);
        bool SuppliersExists(int id);
        bool CreateSuppliers(Suppliers suppliers_create);
        bool UpdateSuppliers(Suppliers suppliers_update);
        bool DeleteSuppliers(Suppliers suppliers_delete);
        bool Save();
    }
}
