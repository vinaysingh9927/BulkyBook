using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]  //explicity - not required it automatically find
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork db) 
        {
            _unitOfWork = db; 
        }

        public IActionResult Index()
        { 
            IEnumerable<Product> Product = _unitOfWork.Product.GetAll(); //to use unityof work.
            return View(Product);
        }

        //Edit/Update Functionality 

        //GET
        public IActionResult Upsert(int? id)  //upsert (create/update=upsert)
        {
            Product product = new();
            if (id ==null || id==0)
            {
                //create product
                return NotFound(product);
            }
            else
            {
                //update product
            }

            return View(product); 
        }
        
        //Post
        [HttpPost]
        [AutoValidateAntiforgeryToken]  
        public IActionResult Upsert(CoverType obj)  //in validation check model is valid or not (Require properties have or not)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj); 
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
            
            var ProductFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u=>u.Id == id); 

            if (ProductFromDbFirst == null)
            {
                return NotFound();
            }
            return View(ProductFromDbFirst); 
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
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index"); 
        }
    }
}
