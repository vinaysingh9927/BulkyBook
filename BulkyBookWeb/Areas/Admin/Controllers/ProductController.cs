using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
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
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll(); //to use unityof work.
            return View(objCoverTypeList);
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
        public IActionResult Create(CoverType obj)  //in validation check model is valid or not (Require properties have or not)
        {
            if (ModelState.IsValid)   
            {
                _unitOfWork.CoverType.Add(obj);  
                _unitOfWork.Save();
                TempData["success"] = "CoverType created successfully";
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
            
            var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u=>u.Id == id); 
           
            if (CoverTypeFromDbFirst == null) 
            {
                return NotFound();
            }
            return View(CoverTypeFromDbFirst); 
        }
        //Post
        [HttpPost]
        [AutoValidateAntiforgeryToken] 
        public IActionResult Edit(CoverType obj)  //in validation check model is valid or not (Require properties have or not)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj); 
                _unitOfWork.Save(); 
                TempData["success"] = "CoverType updated successfully";
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
}
