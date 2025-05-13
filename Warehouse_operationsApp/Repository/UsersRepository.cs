using AutoMapper;
using Warehouse_operationsApp.Data;
using Warehouse_operationsApp.Models;
using Warehouse_operationsApp.Repository.Interfaces;

namespace Warehouse_operationsApp.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UsersRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateUsers(int id_doljnosti, Users Users_create)
        {
            _context.Add(Users_create);
            return Save();
        }

        public bool DeleteUsers(Users Users_delete)
        {
            _context.Remove(Users_delete);
            return Save();
        }

        public ICollection<Users> GetDoljnostiByUsers(int Id_doljnosti)
        {
            return _context.Userss.Where(r => r.Doljnosti.id_doljnosti == Id_doljnosti).ToList();
        }

        public Users GetUsersById(int User_id)
        {
            return _context.Userss.Where(r => r.id_users == User_id).FirstOrDefault();
        }

        public ICollection<Users> GetUsersList()
        {
            return _context.Userss.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUsers(int id_doljnosti, Users Users_update)
        {
            _context.Update(Users_update);
            return Save();
        }

        public bool UsersExists(int User_id)
        {
            return _context.Userss.Any(r => r.id_users == User_id);
        }
    }
}
