using Warehouse_operationsApp.Models;

namespace Warehouse_operationsApp.Repository.Interfaces
{
    public interface IUsersRepository
    {
        ICollection<Users> GetUsersList();
        Users GetUsersById(int User_id);
        ICollection<Users> GetDoljnostiByUsers(int Id_doljnosti);
        bool UsersExists(int User_id);
        bool CreateUsers(int id_doljnosti, Users Users_create);
        bool UpdateUsers(int id_doljnosti, Users Users_update);
        bool DeleteUsers(Users Users_delete);
        bool Save();
    }
}
