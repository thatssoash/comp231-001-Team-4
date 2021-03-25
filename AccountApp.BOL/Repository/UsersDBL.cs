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
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
