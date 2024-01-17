﻿using OperatorManagementBL.DTOs;
using OperatorManagementBL.Services;
using System.Net;
using System.Web.Mvc;
using OperatorManagementBL.Exceptions;

namespace OperatorManagementUI.Controllers
{
    public class PersonController : Controller
    {

        private readonly IPersonService _personService;

        public PersonController()
        {
            _personService = new PersonService();
        }


        public ActionResult Index()
        {
            return View(_personService.GetPeople());
        }

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
            catch
            {
                return HttpNotFound();
            }
        }


        public ActionResult Create()
        {
            return View();
        }

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
            catch(DuplicateNationCodeException ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
            catch
            {
                return HttpNotFound();
            }
        }

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
            catch
            {
                return HttpNotFound();
            }
        }

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
            catch (System.Exception)
            {
                return HttpNotFound();
            }
        }

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
            catch
            {
                return HttpNotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _personService.DeletePersonById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public ActionResult DeletedPeople()
        {
            return View(_personService.GetDeletedPeople());
        }

        public ActionResult UnDelete(int id)
        {
            try
            {
                _personService.UnDeletePersonById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}
