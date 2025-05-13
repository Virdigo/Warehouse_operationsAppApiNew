using Warehouse_operationsApp.Models;

namespace Warehouse_operationsApp.Repository.Interfaces
{
    public interface IDoljnostiRepository
    {
        ICollection<Doljnosti> GetDoljnostisList();

        Doljnosti GetDoljnostiById(int Id_doljnosti);

        Doljnosti GetDoljnosti(string post);
        string GetUserByIdDoljnosti(int doljnosId);
        bool DoljnostiExists(int doljnosId);
        bool CreateDoljnosti(Doljnosti doljnosti_create);
        bool UpdateDoljnosti(Doljnosti doljnosti_update);
        bool DeleteDoljnosti(Doljnosti doljnosti_delete);
        bool Save();
    }
}
