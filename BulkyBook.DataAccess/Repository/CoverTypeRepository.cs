using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository 
    {
        private readonly ApplicationDbContext _db;
         
        public CoverTypeRepository(ApplicationDbContext db) : base(db) //pass db to base class also
        {
            _db = db;
        }

        /*public void Save()  //shift it to the UnitOfWork
        {
            _db.SaveChanges();
        }*/
         
        public void Update(CoverType obj)
        {
            _db.CoverTypes.Update(obj); 
        }
    }
}
