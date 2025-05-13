using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Warehouse_operationsApp.Data;
using Warehouse_operationsApp.Models;
using Warehouse_operationsApp.Repository.Interfaces;

namespace Warehouse_operationsApp.Repository
{
    public class Information_about_documentsRepository : IInformation_about_documentsRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Information_about_documentsRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ICollection<Information_about_documents> GetInformation_About_DocumentssList()
        {
            return _context.Information_About_Documentss
                .Include(d => d.Product)
                .Include(d => d.Receipt_and_expense_documents)
                .Include(d => d.Suppliers)
                .ToList();
        }

        public Information_about_documents GetInformation_About_DocumentssById(int id_inf)
        {
            return _context.Information_About_Documentss
                .Include(d => d.Product)
                .Include(d => d.Receipt_and_expense_documents)
                .Include(d => d.Suppliers)
                .FirstOrDefault(r => r.id_inf_doc == id_inf);
        }

        public ICollection<Information_about_documents> GetInformation_About_DocumentssByProduct(int ProductID)
        {
            return _context.Information_About_Documentss.Where(r => r.Product.id_Product == ProductID).ToList();
        }

        public bool Information_about_documentsExists(int id_inf_doc)
        {
            return _context.Information_About_Documentss.Any(r => r.id_inf_doc == id_inf_doc);
        }

        public bool CreateInformation_about_documents(int ProductID, int id_doc, int id_suppliers, Information_about_documents Information_about_documents_create)
        {
            _context.Add(Information_about_documents_create);
            return Save();
        }

        public bool UpdateInformation_about_documents(int ProductID, int id_doc, int id_suppliers, Information_about_documents Information_about_documents_update)
        {
            _context.Update(Information_about_documents_update);
            return Save();
        }

        public bool DeleteInformation_about_documents(Information_about_documents Information_about_documents_delete)
        {
            _context.Remove(Information_about_documents_delete);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
