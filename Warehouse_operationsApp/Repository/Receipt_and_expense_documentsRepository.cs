using Microsoft.EntityFrameworkCore;
using Warehouse_operationsApp.Data;
using Warehouse_operationsApp.Models;
using Warehouse_operationsApp.Repository.Interfaces;

namespace Warehouse_operationsApp.Repository
{
    public class Receipt_and_expense_documentsRepository : IReceipt_and_expense_documentsRepository
    {
        private readonly DataContext _context;

        public Receipt_and_expense_documentsRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Information_about_documents> GetInformationByDocuments(int id_doc)
        {
            return _context.Information_About_Documentss.Where(p => p.id_doc == id_doc).ToList();
        }

        public ICollection<Receipt_and_expense_documents> GetReceipt_and_expense_documentsList()
        {
            return _context.Receipt_And_Expense_Documentss
        .Include(r => r.Users) // Подгружаем пользователя
        .ToList();
        }

        public Receipt_and_expense_documents GetReceipt_and_expense_documentsById(int id_doc)
        {
            return _context.Receipt_And_Expense_Documentss
        .Include(r => r.Users) // Подгружаем пользователя
        .FirstOrDefault(o => o.id_doc == id_doc);
        }

        public bool Receipt_and_expense_documentsExists(int id_doc)
        {
            return _context.Receipt_And_Expense_Documentss.Any(o => o.id_doc == id_doc);
        }

        public bool CreateReceipt_and_expense_documents(int id_users, Receipt_and_expense_documents Receipt_and_expense_documents_create)
        {
            _context.Add(Receipt_and_expense_documents_create);
            return Save();
        }

        public bool UpdateReceipt_and_expense_documents(int id_users, Receipt_and_expense_documents Receipt_and_expense_documents_update)
        {
            _context.Update(Receipt_and_expense_documents_update);
            return Save();
        }

        public bool DeleteReceipt_and_expense_documents(Receipt_and_expense_documents Receipt_and_expense_documents_delete)
        {
            _context.Remove(Receipt_and_expense_documents_delete);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
