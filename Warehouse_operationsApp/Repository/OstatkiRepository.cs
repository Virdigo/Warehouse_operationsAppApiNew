using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Warehouse_operationsApp.Data;
using Warehouse_operationsApp.Models;
using Warehouse_operationsApp.Repository.Interfaces;

namespace Warehouse_operationsApp.Repository
{
    public class OstatkiRepository : IOstatkiRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OstatkiRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateOstatki(int id_warehouses, int ProductId, Ostatki Ostatki_create)
        {
            _context.Add(Ostatki_create);
            return Save();
        }

        public bool DeleteOstatki(Ostatki Ostatki_delete)
        {
            _context.Remove(Ostatki_delete);
            return Save();
        }

        public Ostatki GetOstatkiById(int OstatkiId)
        {
            return _context.Ostatkis
            .Include(o => o.Product)
            .Include(o => o.Warehouses)
            .FirstOrDefault(o => o.id_Ostatki == OstatkiId);
        }

        public ICollection<Ostatki> GetOstatkisList()
        {
            return _context.Ostatkis
            .Include(o => o.Product) // Подключаем информацию о продукте
            .Include(o => o.Warehouses) // Подключаем информацию о складе
            .ToList();
        }

        public ICollection<Ostatki> GetOstatkisOfProduct(int ProductId)
        {
            return _context.Ostatkis.Where(r => r.Product.id_Product == ProductId).ToList();
        }

        public bool OstatkiExists(int OstatkiId)
        {
            return _context.Ostatkis.Any(r => r.id_Ostatki == OstatkiId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOstatki(int id_warehouses, int ProductId, Ostatki Ostatki_update)
        {
            _context.Update(Ostatki_update);
            return Save();
        }
    }
}
