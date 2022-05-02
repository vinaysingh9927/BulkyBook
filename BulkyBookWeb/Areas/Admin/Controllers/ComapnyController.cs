using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]  //explicity - not required it automatically find
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
         
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll(); //to use unityof work.
            return View();
        }
            

        //Update+Insert both Functionality in same action Upsert

        //GET
        public IActionResult Upsert(int? id)  //insert+update = upsert 
        {
            Company company = new();          

            if (id == null || id == 0)
            {
                return View(company);
            }
            else
            {
                //update product
                company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);
            }
        }

        //Post
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(Company obj)  //in validation check model is valid or not (Require properties have or not)
        {
            if (ModelState.IsValid)
            {

                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Company updated successfully";
                }
                _unitOfWork.Save();
                
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        

        //api call

        #region API CALLS
        [HttpGet] 
        public IActionResult GetAll()
        {
            var comapnyList = _unitOfWork.Company.GetAll();
            return Json(new { data = comapnyList });
        }

        //Post
        [HttpDelete]
        /*[AutoValidateAntiforgeryToken] */
        public IActionResult Delete(int? id) 
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id); 
            if (obj == null)
            {
                return Json(new { success = false,message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successful" });
        }
        #endregion
    }
}