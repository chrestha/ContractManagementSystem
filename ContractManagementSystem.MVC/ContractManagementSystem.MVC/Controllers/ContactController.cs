using ContractManagementSystem.BusinessLogic.DataManager;
using ContractManagementSystem.BusinessLogic.DataManager.Interface;
using ContractManagementSystem.Repository.Interfaces;
using ContractManagementSystem.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContractManagementSystem.MVC.Controllers
{
    public class ContactController : Controller
    {
        protected IContactDM _service = null;
        protected IUnitOfWork _unitOfWork = null;
       

        public ContactController(IUnitOfWork unitOfWork , IContactDM service )
        {
            _unitOfWork = unitOfWork ;
            _service = service ;
        }

        //// GET: Student
        //public ActionResult Index()
        //{
        //    SimpleMoldelFilter<StudentVM> model = new SimpleMoldelFilter<StudentVM>();
        //    model = _service.GetFilter(model);
        //    ViewBag.ClassId = new SelectList(new ClassRoomDM().GetAll(), "Id", "Name");
        //    return View(model);
        //}
        //public ActionResult Filter(SimpleMoldelFilter<StudentVM> model)
        //{
        //    ViewBag.ClassId = new SelectList(new ClassRoomDM().GetAll(), "Id", "Name");
        //    return View("Index", _service.GetFilter(model));
        //}

        //// GET: Student/Details/5
        //public ActionResult Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StudentVM student = _service.GetById(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}
        //public ActionResult Results(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    List<StudentResultVM> studentResults = _service.GetExamListByStudentId(id ?? 0);
        //    if (studentResults == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(studentResults);
        //}

        //public ActionResult Result(long? studentId, long? examId, int? position)
        //{
        //    if (studentId == null || examId == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StudentResultVM studentResult = _service.GetStudentResult(examId ?? 0, studentId ?? 0, position ?? 0);
        //    if (studentResult == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(studentResult);
        //}


        //// GET: Student/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ClassRoomId = new SelectList(new ClassRoomDM().GetAll(), "Id", "Name");
        //    return View();
        //}

        //// POST: Student/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(StudentVM student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _service.Insert(student);
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ClassRoomId = new SelectList(new ClassRoomDM().GetAll(), "Id", "Name");
        //    return View(student);
        //}

        //// GET: Student/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StudentVM student = _service.GetById(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ClassRoomId = new SelectList(new ClassRoomDM().GetAll(), "Id", "Name", student.ClassRoomId);
        //    return View(student);
        //}

        //// POST: Student/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(StudentVM student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _service.Update(student);
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ClassRoomId = new SelectList(new ClassRoomDM().GetAll(), "Id", "Name", student.ClassRoomId);
        //    return View(student);
        //}

        //// GET: Student/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StudentVM student = _service.GetById(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        //// POST: Student/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(StudentVM student)
        //{
        //    try
        //    {
        //        _service.Delete(student);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //    return View(student);
        //}
    }
}