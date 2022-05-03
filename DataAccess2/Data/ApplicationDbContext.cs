using BulkyBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess 
{
    public class ApplicationDbContext : IdentityDbContext 
    {
        //create constructor having options and pass to the base class (DbContext)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

    } 
}  
