using Warehouse_operationsApp.Models;

namespace Warehouse_operationsApp.Repository.Interfaces
{
    public interface IUnitRepository
    {
        ICollection<Unit> GetUnitsList();
        Unit GetUnitsById(int idUnit);
        ICollection<Unit> GetUnitsByProduct(int ProductID);
        bool UnitExists(int idUnit);
        bool CreateUnit(Unit Unit_create);
        bool UpdateUnit(Unit Unit_update);
        bool DeleteUnit(Unit Unit_delete);
        bool Save();
    }
}
