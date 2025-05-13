using Warehouse_operationsApp.Models;

namespace Warehouse_operationsApp.Repository.Interfaces
{
    public interface IInformation_about_documentsRepository
    {
        ICollection<Information_about_documents> GetInformation_About_DocumentssList();
        Information_about_documents GetInformation_About_DocumentssById(int id_inf);
        ICollection<Information_about_documents> GetInformation_About_DocumentssByProduct(int ProductID);
        bool Information_about_documentsExists(int id_inf);
        bool CreateInformation_about_documents(int ProductID, int id_doc, int id_suppliers, Information_about_documents Information_about_documents_create);
        bool UpdateInformation_about_documents(int ProductID, int id_doc, int id_suppliers, Information_about_documents Information_about_documents_update);
        bool DeleteInformation_about_documents(Information_about_documents Information_about_documents_delete);
        bool Save();
    }
}
