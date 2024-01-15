using OperatorManagementBL.DTOs;
using OperatorManagementBL.Services;
using System.Net;
using System.Web.Mvc;

namespace OperatorManagementUI.Controllers
{
    public class SimController : Controller
    {
        private readonly ISimService _simService;
        private readonly IPersonService _personService;

        public SimController()
        {
            _simService = new SimService();
            _personService = new PersonService();
        }

        public ActionResult Index()
        {
            return View(_simService.GetSims());
        }

        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SimDTO sim = _simService.GetSimById(id.Value);
                if (sim == null)
                {
                    return HttpNotFound();
                }
                return View(sim);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public ActionResult Create()
        {
            ViewBag.Person_Id = new SelectList(_personService.GetPeople(), "Id", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Number,Person_Id")] SimDTO sim)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _simService.AddSim(sim);
                    return RedirectToAction("Index");
                }

                ViewBag.Person_Id = new SelectList(_personService.GetPeople(), "Id", "FirstName", sim.Person_Id);
                return View(sim);
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
                SimDTO sim = _simService.GetSimById(id.Value);
                if (sim == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Person_Id = new SelectList(_personService.GetPeople(), "Id", "FirstName", sim.Person_Id);
                return View(sim);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Number,Person_Id")] SimDTO sim)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _simService.UpdateSim(sim);
                    return RedirectToAction("Index");
                }
                ViewBag.Person_Id = new SelectList(_personService.GetPeople(), "Id", "FirstName", sim.Person_Id);
                return View(sim);
            }
            catch
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
                SimDTO sim = _simService.GetSimById(id.Value);
                if (sim == null)
                {
                    return HttpNotFound();
                }
                return View(sim);
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
                _simService.DeleteSimById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}
