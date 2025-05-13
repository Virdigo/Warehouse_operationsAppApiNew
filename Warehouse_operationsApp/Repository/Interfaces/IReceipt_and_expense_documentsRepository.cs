using Warehouse_operationsApp.Models;

namespace Warehouse_operationsApp.Repository.Interfaces
{
    public interface IReceipt_and_expense_documentsRepository
    {
        ICollection<Receipt_and_expense_documents> GetReceipt_and_expense_documentsList();
        Receipt_and_expense_documents GetReceipt_and_expense_documentsById(int id);
        ICollection<Information_about_documents> GetInformationByDocuments(int id_doc);
        bool Receipt_and_expense_documentsExists(int id_doc);
        bool CreateReceipt_and_expense_documents(int id_users, Receipt_and_expense_documents Receipt_and_expense_documents_create);
        bool UpdateReceipt_and_expense_documents(int id_users, Receipt_and_expense_documents Receipt_and_expense_documents_update);
        bool DeleteReceipt_and_expense_documents(Receipt_and_expense_documents Receipt_and_expense_documents_delete);
        bool Save();
    }
}
