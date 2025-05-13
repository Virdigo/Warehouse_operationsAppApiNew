using AutoMapper;
using Warehouse_operationsApp.Data;
using Warehouse_operationsApp.Models;
using Warehouse_operationsApp.Repository.Interfaces;

namespace Warehouse_operationsApp.Repository
{
    public class UnitRepository : IUnitRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UnitRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ICollection<Unit> GetUnitsList()
        {
            return _context.Units.ToList();
        }

        public Unit GetUnitsById(int idUnit)
        {
            return _context.Units.Where(r => r.id_unit == idUnit).FirstOrDefault();
        }

        public ICollection<Unit> GetUnitsByProduct(int ProductID)
        {
            return _context.Products.Where(p => p.id_Product == ProductID).Select(p => p.Unit).Distinct().ToList();
        }

        public bool UnitExists(int idUnit)
        {
            return _context.Units.Any(r => r.id_unit == idUnit);
        }

        public bool CreateUnit(Unit Unit_create)
        {
            _context.Add(Unit_create);
            return Save();
        }

        public bool UpdateUnit(Unit Unit_update)
        {
            _context.Update(Unit_update);
            return Save();
        }

        public bool DeleteUnit(Unit Unit_delete)
        {
            _context.Remove(Unit_delete);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
