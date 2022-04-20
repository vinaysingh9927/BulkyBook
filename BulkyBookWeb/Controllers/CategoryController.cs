using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //Create Functionality
        //GET
        public IActionResult Create() 
        {
            return View();
        }
        //Post
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Category obj)  //in validation check model is valid or not (Require properties have or not)
        {
            //costom validation
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                //key       error
                //ModelState.AddModelError("CustomError","The DisplayOrder cannot be exactly match the Name.");
                ModelState.AddModelError("Name", "The DisplayOrder cannot be exactly match the Name.");
            }


            //check properties validation 
            if (ModelState.IsValid)     //havor on ModelState and check(Values>Result Values) if any propertie valid or not here 
            {
                _db.Categories.Add(obj);   
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index"); 
            }
            return View(obj);
        }



        //Edit/Update Functionality 

        //GET
        public IActionResult Edit(int? id) 
        {
            if (id ==null || id==0)
            {
                return NotFound();
            }
            //way of retreive category
            var categoryFromDb = _db.Categories.Find(id);  //based on the primary key it find  
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.ID == id); //not throw exception and return first element of the list 
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.ID == id); //throw an exception
            if (categoryFromDb == null) 
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //Post
        [HttpPost]
        [AutoValidateAntiforgeryToken] 
        public IActionResult Edit(Category obj)  //in validation check model is valid or not (Require properties have or not)
        {
            //costom validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //key       error
                //ModelState.AddModelError("CustomError","The DisplayOrder cannot be exactly match the Name.");
                ModelState.AddModelError("Name", "The DisplayOrder cannot be exactly match the Name.");

            }


            //check properties validation 
            if (ModelState.IsValid)     //havor on ModelState and check(Values>Result Values) if any propertie valid or not here 
            {
                _db.Categories.Update(obj); //update for this 
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //update functionality
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //way of retreive category
            var categoryFromDb = _db.Categories.Find(id);  //based on the primary key it find  
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.ID == id); //not throw exception and return first element of the list 
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.ID == id); //throw an exception
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //Post
        [HttpPost,ActionName("Delete")]
        [AutoValidateAntiforgeryToken]  
        public IActionResult DeletePOST(int? id)  //in validation check model is valid or not (Require properties have or not)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj); //remove for this 
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index"); 
        }
    }
}
