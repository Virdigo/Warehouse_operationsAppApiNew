using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Warehouse_operationsApp.Data;
using Warehouse_operationsApp.Models;
using Warehouse_operationsApp.Repository.Interfaces;

namespace Warehouse_operationsApp.Repository
{
    public class WarehousesRepository : IWarehousesRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public WarehousesRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateWarehouses(int id_users, Warehouses Warehouses_create)
        {
            _context.Add(Warehouses_create);
            return Save();
        }

        public bool DeleteWarehouses(Warehouses Warehouses_delete)
        {
            _context.Remove(Warehouses_delete);
            return Save();
        }

        public ICollection<Warehouses> GetUsersByWarehouses(int User_id)
        {
            return _context.Warehousess.Where(r => r.Users.id_users == User_id).ToList();
        }

        public Warehouses GetWarehousesById(int WarehousesId)
        {
            return _context.Warehousess
        .Include(w => w.Users) // Подгружаем пользователя
        .FirstOrDefault(r => r.id_warehouses == WarehousesId);
        }

        public ICollection<Warehouses> GetWarehousesList()
        {
            return _context.Warehousess
        .Include(w => w.Users) // Подгружаем пользователя
        .ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateWarehouses(int id_users, Warehouses Warehouses_update)
        {
            _context.Update(Warehouses_update);
            return Save();
        }

        public bool WarehousesExists(int WarehousesId)
        {
            return _context.Warehousess.Any(r => r.id_warehouses == WarehousesId);
        }
    }
}
