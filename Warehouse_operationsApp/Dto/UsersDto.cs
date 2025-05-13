using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse_operationsApp.Dto
{
    public class UsersDto
    {
        public int id_users { get; set; }
        public string FIO { get; set; }
        public string Login { get; set; }
        public string password { get; set; }
        public int id_doljnosti { get; set; }
    }
}
