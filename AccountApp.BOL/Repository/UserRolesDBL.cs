using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountApp.BOL.Repository
{
    /// <summary>
    /// Interface to expose methods on Roles
    /// </summary>
    public interface IUserRolessDBL
    {
        IEnumerable<UserRoles> GetUserRoles();
        UserRoles GetUserRole(string name);
        UserRoles GetUserRole(int id);
        bool Delete(int id);
        bool Save(UserRoles userRoles);
        bool Update(UserRoles userRoles);
    }

    /// <summary>
    /// Base class 
    /// </summary>
    public class UserRolesDBL : IUserRolessDBL
    {
        private AccountAppDbContext _db;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db">Injecting DbContext dependency</param>
        public UserRolesDBL(AccountAppDbContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Delete Role with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Role type based on Role name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UserRoles GetUserRole(string name)
        {
            return _db.UserRoles.FirstOrDefault(x=>x.RoleName==name);
        }

        /// <summary>
        /// get user role based on role id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public UserRoles GetUserRole(int id)
        {
            return _db.UserRoles.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Get Complete Roles list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserRoles> GetUserRoles()
        {
            return _db.UserRoles;
        }

        /// <summary>
        /// Save new role to db
        /// </summary>
        /// <param name="userRoles"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="userRoles"></param>
        /// <returns></returns>
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
