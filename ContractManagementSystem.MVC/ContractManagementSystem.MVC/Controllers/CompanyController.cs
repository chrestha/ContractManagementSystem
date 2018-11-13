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
        protected ICompanyDM _service = null;
        public CompanyController() : base()
        {             
            _service = new CompanyDM();
        }

        public CompanyController(CompanyDM service = null)
        {             
            _service = service ?? new CompanyDM();
        }
       
        // GET: Company
        public ActionResult Index()
        {
            int maxRows = 0;
            List<CompanyVM> list = _service.GetList("",1,10, out maxRows);
            return View(list);
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
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

        // POST: Company/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CompanyVM collection)
        {
            try
            {
                _service.Delete(collection.ID);
                return RedirectToAction("Index");
            }
            catch
            {
               
            }
            return View(collection);
        }
    }
}
