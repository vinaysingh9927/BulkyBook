using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess 
{
    public class ApplicationDbContext : DbContext
    {
        //create constructor having options and pass to the base class (DbContext)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //each model we have to create DbSet
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }  //CoverTypes table name created in database
        public DbSet<Product> Products { get; set; }  //Product table name created in database
    } 
}  
