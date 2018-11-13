using ContractManagementSystem.BusinessLogic.DataManager;
using ContractManagementSystem.BusinessLogic.DataManager.Interface;
using ContractManagementSystem.BusinessLogic.Helper;
using ContractManagementSystem.BusinessLogic.ViewModel;
using ContractManagementSystem.Repository.Interfaces;
using ContractManagementSystem.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ContractManagementSystem.MVC.Controllers
{
    public class ContactController : Controller
    {
        protected IContactDM _service = null;
        protected ICompanyDM _serviceCompany = null;
        protected IUnitOfWork _unitOfWork = null;
       

        public ContactController(IUnitOfWork unitOfWork , IContactDM service , ICompanyDM serviceCompany)
        {
            _unitOfWork = unitOfWork ;
            _service = service;
            _serviceCompany = serviceCompany;
        }

        // GET: Contact
        public ActionResult Index()
        {
            SimpleMoldelFilter<ContactVM> model = new SimpleMoldelFilter<ContactVM>();
            model = _service.GetFilter(model);
             
            return View(model);
        }
        public ActionResult Filter(SimpleMoldelFilter<ContactVM> model)
        {
           
            return View("Index", _service.GetFilter(model));
        }

        // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactVM contact = _service.GetById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }
       

        // GET: Contact/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(_serviceCompany.GetFilteredList(), "ID", "Name");
            ViewBag.TitleId = new SelectList(_service.GetTitles(), "Id", "Title");
            return View();
        }

        // POST: Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactVM contact)
        {
            if (ModelState.IsValid)
            {
                _service.Insert(contact);
                return RedirectToAction("Index");
            }
            
            ViewBag.CompanyId = new SelectList(_serviceCompany.GetFilteredList(), "ID", "Name");
            ViewBag.TitleId = new SelectList(_service.GetTitles(), "Id", "Title");
            return View(contact);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactVM contact = _service.GetById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(_serviceCompany.GetFilteredList(), "ID", "Name");
            ViewBag.TitleId = new SelectList(_service.GetTitles(), "Id", "Title");
            return View(contact);
        }

        // POST: Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactVM contact)
        {
            if (ModelState.IsValid)
            {
                _service.Update(contact);
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(_serviceCompany.GetFilteredList(), "ID", "Name");
            ViewBag.TitleId = new SelectList(_service.GetTitles(), "Id", "Title");
            return View(contact);
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           _service.Delete(id);
             
            return RedirectToAction("Index");
        }
        public ActionResult ChangeStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _service.ChangeStatus(id);

            return RedirectToAction("Index");
        }

    }
}