using OperatorManagementBL.DTOs;
using OperatorManagementBL.Services;
using System;
using System.Net;
using System.Web.Mvc;

namespace OperatorManagementUI.Controllers
{
    public class PersonController : Controller
    {

        private readonly IPersonService _personService;

        public PersonController()
        {
            _personService = new PersonService();
        }


        /// <summary>
        /// صفحه لیست اشخاص
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(_personService.GetPeople());
        }

        /// <summary>
        /// صفحه جزئیات شخص
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PersonDTO person = _personService.GetPersonById(id.Value);
                if (person == null)
                {
                    return HttpNotFound();
                }
                return View(person);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }

        /// <summary>
        ///  صفحه ایجاد شخص
        ///  GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// صفحه ایجاد شخص
        /// POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] PersonDTO tbl_Person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _personService.AddPerson(tbl_Person);
                    return RedirectToAction("Index");
                }

                return View(tbl_Person);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }

        /// <summary>
        /// صفحه ویرایش شخص
        /// GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PersonDTO person = _personService.GetPersonById(id.Value);
                if (person == null)
                {
                    return HttpNotFound();
                }
                return View(person);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }

        /// <summary>
        /// صفحه ویرایش شخص
        /// POST
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind] PersonDTO person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _personService.UpdatePerson(person);
                    return RedirectToAction("Index");
                }
                return View(person);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }

        /// <summary>
        /// صفحه حذف شخص
        /// GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PersonDTO person = _personService.GetPersonById(id.Value);
                if (person == null)
                {
                    return HttpNotFound();
                }
                return View(person);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }

        /// <summary>
        /// صفحه حذف شخص
        /// POST
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _personService.DeletePersonById(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }

        /// <summary>
        /// صفحه لیست اشخاص حذف شده
        /// </summary>
        /// <returns></returns>

        public ActionResult DeletedPeople()
        {
            return View(_personService.GetDeletedPeople());
        }

        /// <summary>
        /// صفحه بازگردانی شخص حذف شده
        /// </summary>
        /// <returns></returns>
        public ActionResult UnDelete(int id)
        {
            try
            {
                _personService.UnDeletePersonById(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }
    }
}
