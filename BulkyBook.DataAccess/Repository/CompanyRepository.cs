using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db) //pass db to base class also
        {
            _db = db;
        }

        /*public void Save()  //shift it to the UnitOfWork
        {
            _db.SaveChanges();
        }*/
         
        public void Update(Company obj) 
        {
            _db.Companies.Update(obj);
        }
    }
}
