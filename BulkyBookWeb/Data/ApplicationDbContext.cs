using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        //create constructor having options and pass to the base class (DbContext)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //each model we have to create DbSet
        public DbSet<Category> Categories { get; set; }
    }
}
