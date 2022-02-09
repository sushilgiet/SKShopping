using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebView.Controllers
{
    public class EventGridController : Controller
    {
        // GET: EventGridController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EventGridController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventGridController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventGridController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventGridController/Edit/5
        public ActionResult Se(int id)
        {
            return View();
        }

        // POST: EventGridController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventGridController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventGridController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
