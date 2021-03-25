using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountApp.BOL.Repository
{
    public interface IUsersDBL
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsers(string name);
        User GetUser(int id);
        bool Delete(int id);
        bool Save(User user);
        bool Update(User user);
    }
    public class UsersDBL : IUsersDBL
    {
        private AccountAppDbContext _db;
        public UsersDBL(AccountAppDbContext db)
        {
            this._db = db;
        }
        public bool Delete(int id)
        {
            try
            {
                var user = _db.Users.Find(id);
                _db.Users.Remove(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public User GetUser(int id)
        {
            return _db.Users.Find(id); ;
        }

        public IEnumerable<User> GetUsers()
        {
            return this._db.Users;
        }

        public IEnumerable<User> GetUsers(string name)
        {
            return this._db.Users.Where(x=>x.Name.Contains(name));
        }

        public bool Save(User user)
        {
            try
            {
                this._db.Users.Add(user);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Update(User user)
        {
            try
            {
                _db.Update<User>(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
