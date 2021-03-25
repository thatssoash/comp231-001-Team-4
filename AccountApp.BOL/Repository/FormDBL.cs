using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountApp.BOL.Repository
{
    public interface IFormDBL
    {
        IEnumerable<CustomForm> GetCustomForms();
        IEnumerable<CustomForm> GetCustomForms(string name);
        CustomForm GetCustomForm(int id);
        bool Delete(int id);
        bool Save(CustomForm form);
        bool Update(CustomForm form);
    }

    public class FormDBL : IFormDBL
    {
        private AccountAppDbContext _db;
        public FormDBL(AccountAppDbContext db)
        {
            this._db = db;
        }

        public bool Delete(int id)
        {
            try
            {
                var form = _db.CustomForms.Find(id);
                _db.CustomForms.Remove(form);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public CustomForm GetCustomForm(int id)
        {
            return _db.CustomForms.Find(id);
        }

        public IEnumerable<CustomForm> GetCustomForms()
        {
            return _db.CustomForms;
        }

        public IEnumerable<CustomForm> GetCustomForms(string name)
        {
            return _db.CustomForms.Where(x=>x.FormName.Contains(name));
        }

        public bool Save(CustomForm form)
        {
            try
            {
                _db.CustomForms.Add(form);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Update(CustomForm form)
        {
            try
            {
                _db.Update<CustomForm>(form);
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
