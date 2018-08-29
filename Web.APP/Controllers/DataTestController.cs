using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.APP.Controllers
{
    public class DataTestController : Controller
    {
        protected InventoryContext DbContext;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            DbContext = DbContextFactory.Create("testdb");
            base.OnActionExecuting(context);
        }
        // GET: DataTest
        public ActionResult Index()
        {
            return View(DbContext.Channel);
        }

        // GET: DataTest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DataTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DataTest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DataTest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DataTest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DataTest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DataTest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}