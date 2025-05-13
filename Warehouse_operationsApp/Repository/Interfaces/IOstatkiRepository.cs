using Warehouse_operationsApp.Models;

namespace Warehouse_operationsApp.Repository.Interfaces
{
    public interface IOstatkiRepository
    {
        ICollection<Ostatki> GetOstatkisList();
        Ostatki GetOstatkiById(int OstatkiId);
        ICollection<Ostatki> GetOstatkisOfProduct(int ProductId);
        bool OstatkiExists(int OstatkiId);
        bool CreateOstatki(int id_warehouses, int ProductId, Ostatki Ostatki_create);
        bool UpdateOstatki(int id_warehouses, int ProductId, Ostatki Ostatki_update);
        bool DeleteOstatki(Ostatki Ostatki_delete);
        bool Save();
    }
}
