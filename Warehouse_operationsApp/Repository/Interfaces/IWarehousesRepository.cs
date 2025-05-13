using Warehouse_operationsApp.Models;

namespace Warehouse_operationsApp.Repository.Interfaces
{
    public interface IWarehousesRepository
    {
        ICollection<Warehouses> GetWarehousesList();
        Warehouses GetWarehousesById(int WarehousesId);
        ICollection<Warehouses> GetUsersByWarehouses(int User_id);
        bool WarehousesExists(int WarehousesId);
        bool CreateWarehouses(int id_users, Warehouses Warehouses_create);
        bool UpdateWarehouses(int id_users, Warehouses Warehouses_update);
        bool DeleteWarehouses(Warehouses Warehouses_delete);
        bool Save();
    }
}
