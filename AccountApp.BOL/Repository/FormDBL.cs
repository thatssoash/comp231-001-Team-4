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
            throw new NotImplementedException();
        }

        public CustomForm GetCustomForm(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
