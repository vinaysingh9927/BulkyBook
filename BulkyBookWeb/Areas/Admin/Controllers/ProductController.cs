using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers;
    [Area("Admin")]  //explicity - not required it automatically find
    public class ProductController : Controller
    {
        //private readonly ApplicationDbContext _db;
        //private readonly ICoverTypeRepository _db;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll(); //to use unityof work.
            return View(objProductList);
        }


    //Update+Insert both Functionality in same action Upsert

    //GET
    public IActionResult Upsert(int? id)  //insert+update = upsert 
    {
        Product Product = new();

           IEnumerable < SelectListItem > CategoryList = _unitOfWork.Category.GetAll().Select(
           u => new SelectListItem
           {
               Text = u.Name,
               Value = u.ID.ToString()
           });
           IEnumerable < SelectListItem > CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
           u => new SelectListItem
           {
               Text = u.Name,
               Value = u.Id.ToString()
           });
           
           
           if (id == null || id == 0)
           {
               //create product
               ViewBag.CategoryList = CategoryList;
           
               return View(Product);
           }
           else
           {
               //update product
           }
           return View(Product);
        }

        //Post
        [HttpPost]
        [AutoValidateAntiforgeryToken] 
        public IActionResult Upsert(Product obj)  //in validation check model is valid or not (Require properties have or not)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj); 
                _unitOfWork.Save(); 
                TempData["success"] = "Product updated successfully";
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
            
            var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u=>u.Id == id); 

            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDbFirst); 
        }

        //Post
        [HttpPost,ActionName("Delete")]
        [AutoValidateAntiforgeryToken]  
        public IActionResult DeletePOST(int? id)  //in validation check model is valid or not (Require properties have or not)
        {
           
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id); //add line after use repo
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(obj);
         
            _unitOfWork.Save();
            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index"); 
        }
    }
