using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountApp.BOL.Repository
{
    public interface IBusinessDBL
    {
        IEnumerable<Business> GetBusinesses();
        IEnumerable<Business> GetBusinesses(string name);
        Business GetBusiness(int id);
        bool Delete(int id);
        bool Save(Business business);
        bool Update(Business business);
    }
    public class BusinessDBL : IBusinessDBL
    {
        private AccountAppDbContext _db;
        public BusinessDBL(AccountAppDbContext db)
        {
            this._db = db;
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Business GetBusiness(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Business> GetBusinesses()
        {
            return _db.Businesses;
        }

        public IEnumerable<Business> GetBusinesses(string name)
        {
            return _db.Businesses.Where(x=>x.BusinessName.Contains(name));
        }

        public bool Save(Business business)
        {
            try
            {
                _db.Businesses.Add(business);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Update(Business business)
        {
            throw new NotImplementedException();
        }
    }
}
