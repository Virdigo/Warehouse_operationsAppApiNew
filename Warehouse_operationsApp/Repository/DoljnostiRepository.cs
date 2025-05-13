using Microsoft.Win32;
using Warehouse_operationsApp.Data;
using Warehouse_operationsApp.Dto;
using Warehouse_operationsApp.Models;
using Warehouse_operationsApp.Repository.Interfaces;

namespace Warehouse_operationsApp.Repository
{
    public class DoljnostiRepository: IDoljnostiRepository
    {
        private readonly DataContext _context;

        public DoljnostiRepository(DataContext context)
        {
            _context = context;
        }

        public bool DoljnostiExists(int doljnosId)
        {
            return _context.Doljnostis.Any(p => p.id_doljnosti == doljnosId);
        }

        public Doljnosti GetDoljnostiById(int Id_doljnosti)
        {
            return _context.Doljnostis.Where(p => p.id_doljnosti == Id_doljnosti).FirstOrDefault();
        }

        public Doljnosti GetDoljnosti(string post)
        {
            return _context.Doljnostis.Where(p => p.Post == post).FirstOrDefault();
        }

        public string GetUserByIdDoljnosti(int doljnosId)
        {
        
            var user = _context.Userss
                .Where(u => u.id_doljnosti == doljnosId)
                .FirstOrDefault();
            if (user == null)
            {
                return "Пользователь не найден";
            }

            return user.FIO;
        }

        public ICollection<Doljnosti> GetDoljnostisList()
        {
            return _context.Doljnostis.OrderBy(p => p.id_doljnosti).ToList();
        }

        public bool CreateDoljnosti(Doljnosti doljnosti_create)
        {
            _context.Add(doljnosti_create);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDoljnosti(Doljnosti doljnosti_update)
        {
            _context.Update(doljnosti_update);
            return Save();
        }

        public bool DeleteDoljnosti(Doljnosti doljnosti_delete)
        {
            _context.Remove(doljnosti_delete);
            return Save();
        }
    }
}
