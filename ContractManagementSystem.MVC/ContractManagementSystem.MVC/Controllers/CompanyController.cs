using ContractManagementSystem.BusinessLogic.DataManager;
using ContractManagementSystem.BusinessLogic.DataManager.Interface;
using ContractManagementSystem.BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ContractManagementSystem.MVC.Controllers
{
    //company Controller for company CURD operation
    public class CompanyController : Controller
    {
        //we can Use Private also
        protected ICompanyDM _service ;        

        public CompanyController(ICompanyDM service )
        {
            this._service = service;
        }
       
        // GET: Company
        [HttpGet]
        public ActionResult Index()
        {            
            List<CompanyVM> list = _service.GetFilteredList("","");
            return View(list);
        }
        [HttpGet]
        public ActionResult _Filter(string Name, string Url)
        {
             
            List<CompanyVM> list = _service.GetFilteredList(Name, Url);
            return PartialView("_Filter", list);
        }
        /// <summary>
        /// This action returns JSON data ie. Company Name and Id for jquery Autocomplete
        /// </summary>
        /// <param name="term">entered value</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCompanyName(string term = "")
        {
            var objDatalist = _service.GetFilteredList().Where(c => !string.IsNullOrEmpty(c.Name))
                            .Where(c => c.Name.ToUpper()
                            .Contains(term.ToUpper()))
                            .Select(c => new { Name = c.Name, ID = c.ID })
                            .Distinct().ToList();
           return Json(objDatalist, JsonRequestBehavior.AllowGet);
        }

        // GET: Company/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            CompanyVM company = new CompanyVM();
            try
            {
                company = _service.GetById(id);              
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(company);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyVM collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.Insert(collection);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
            return View(collection);
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyVM company = _service.GetById(id);
            if (company == null)
            {
                return HttpNotFound();
            }           
            return View(company);
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CompanyVM collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.Update(collection);
                    return RedirectToAction("Index");
                } 
            }
            catch
            {
                
            }
            return View(collection);
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                // only load partial view if result is succes or item is deleted
              int result=  _service.Delete(id);
              return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {

            };
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
       
    }
}
