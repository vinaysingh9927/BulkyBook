using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args); 

// Add services to the container.
builder.Services.AddControllersWithViews();

//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
/*builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection2")
    ));
*/
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaulConnection") //it can also be build seperatlely then pass. 
    )); 

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>(); //add by identity for user

//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); //replace by UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); //use for take care all the Repository
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation(); //for runtime compilation
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); //use to identity of user

app.UseAuthorization();
app.MapRazorPages(); // to map asp-page 
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
