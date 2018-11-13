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
    public class CompanyController : Controller
    {
        protected ICompanyDM _service ;        

        public CompanyController(ICompanyDM service )
        {
            this._service = service;
        }
       
        // GET: Company
        public ActionResult Index()
        {            
            List<CompanyVM> list = _service.GetFilteredList("","");
            return View(list);
        }
        public ActionResult _Filter(string Name, string Url)
        {
             
            List<CompanyVM> list = _service.GetFilteredList(Name, Url);
            return PartialView("_Filter", list);
        }
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
                _service.Delete(id);
               
            }
            catch
            {

            };
            return RedirectToAction("Index");
        }

        //// POST: Company/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, CompanyVM collection)
        //{
        //    try
        //    {
        //        _service.Delete(collection.ID);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
               
        //    }
        //    return View(collection);
        //}
    }
}
