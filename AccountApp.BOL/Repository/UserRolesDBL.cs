using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountApp.BOL.Repository
{
    public interface IUserRolessDBL
    {
        IEnumerable<UserRoles> GetUserRoles();
        UserRoles GetUserRole(string name);
        UserRoles GetUserRole(int id);
        bool Delete(int id);
        bool Save(UserRoles userRoles);
        bool Update(UserRoles userRoles);
    }
    public class UserRolesDBL : IUserRolessDBL
    {
        private AccountAppDbContext _db;
        public UserRolesDBL(AccountAppDbContext db)
        {
            this._db = db;
        }
        public bool Delete(int id)
        {
            try
            {
                var userRoles = _db.UserRoles.Find(id);
                _db.UserRoles.Remove(userRoles);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public UserRoles GetUserRole(string name)
        {
            return _db.UserRoles.FirstOrDefault(x=>x.RoleName==name);
        }

        public UserRoles GetUserRole(int id)
        {
            return _db.UserRoles.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<UserRoles> GetUserRoles()
        {
            return _db.UserRoles;
        }

        public bool Save(UserRoles userRoles)
        {
            try
            {
                _db.UserRoles.Add(userRoles);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Update(UserRoles userRoles)
        {
            try
            {
                _db.Update<UserRoles>(userRoles);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
