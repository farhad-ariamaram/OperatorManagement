using OperatorManagementBL.DTOs;
using OperatorManagementBL.Services;
using System;
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


        // صفحه لیست اشخاص
        public ActionResult Index()
        {
            return View(_personService.GetPeople());
        }

        // صفحه جزئیات شخص
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index", "Error", new ErrorDTO { StatusCode = 404 });
                }

                PersonDetailDTO person = _personService.GetPersonByIdForDetail(id.Value);

                return View(person);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message,StatusCode = 500 });
            }
        }

        //  صفحه ایجاد شخص
        public ActionResult Create()
        {
            return View();
        }

        // صفحه ایجاد شخص
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] PersonDTO person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _personService.AddPerson(person);
                    return RedirectToAction("Index");
                }

                return View(person);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }

        // صفحه ویرایش شخص
        // GET
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index", "Error", new ErrorDTO { StatusCode = 404 });
                }

                PersonDTO person = _personService.GetPersonById(id.Value);

                return View(person);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }

        // صفحه ویرایش شخص
        // POST
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
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }

        // صفحه حذف شخص
        // GET
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index", "Error", new ErrorDTO { StatusCode = 404 });
                }

                PersonDTO person = _personService.GetPersonById(id.Value);

                return View(person);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }

        // صفحه حذف شخص
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _personService.DeletePersonById(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }

        // صفحه لیست اشخاص حذف شده
        public ActionResult DeletedPeople()
        {
            return View(_personService.GetDeletedPeople());
        }

        // صفحه بازگردانی شخص حذف شده
        public ActionResult UnDelete(int id)
        {
            try
            {
                _personService.UnDeletePersonById(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }
    }
}
