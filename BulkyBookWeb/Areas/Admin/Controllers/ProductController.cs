﻿using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]  //explicity - not required it automatically find
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //require for wwwroot folder path
        private readonly IWebHostEnvironment _hostEnvironment; 
         
        public ProductController(IUnitOfWork unitOfWork ,IWebHostEnvironment hostEnvironment)
        {
            //inject by DI
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
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
            //use for dropdown menu 
            //now we use viewbag for transfer the data from controller to view
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ID.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                //create product

                //it(CategoryList) can be any name which we use in view
                /*ViewBag.CategoryList = CategoryList; //add into the viewbag
                ViewData["CoverTypeList"] = CoverTypeList;*/
                return View(productVM);
            }
            else
            {
                //update product
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }
            //return View(productVM);
        }

        //Post
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(ProductVM obj,IFormFile? file)  //in validation check model is valid or not (Require properties have or not)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    //to rename
                    string fileName = Guid.NewGuid().ToString(); 
                    var uploads = Path.Combine(wwwRootPath,@"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    //when we update then we have to check image in database
                    //old image will be remove here
                    if(obj.Product.ImageUrl != null) //existing image in database
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath)) //if find then delete image
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
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

            var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDbFirst);
        }

        //Post
        [HttpPost, ActionName("Delete")]
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

        //api call

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return Json(new { data = productList });
        }
        #endregion
    }
}