using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnicalTest.Models;
using TechnicalTest.Services;

namespace TechnicalTest.Controllers
{
    public class CategoriesController : Controller
    {
        private CategoriesRepository _repo = new CategoriesRepository();

        private enum Alert
        {
            ForeignKey
        }

        // GET: Categories
        public ActionResult Index()
        {
            if (TempData.ContainsKey("Alert"))
            {
                Alert alert = (Alert)TempData["Alert"];
                switch (alert)
                {
                    case Alert.ForeignKey:
                        Utils.SetAlert(ViewBag, "The category \"" + TempData["AlertContent"] + "\" already exists in a product and cannot be removed.", Utils.AlertColors.warning);
                        break;
                    default:
                        break;
                }
            }

            List<Category> categories = _repo.Get();
            return View(categories);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.IsEditing = false;
            return View("CreateAndEdit");
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(category);
                return RedirectToAction("Index");
            }
            ViewBag.IsEditing = false;
            return View("CreateAndEdit",category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _repo.Get(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.IsEditing = true;
            return View("CreateAndEdit",category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _repo.Edit(category);
                return RedirectToAction("Index");
            }
            ViewBag.IsEditing = true;
            return View("CreateAndEdit",category);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Category category = _repo.Get(id);
                if (category == null)
                {
                    return HttpNotFound();
                }

                bool existsInProduct = _repo.ExistsInProduct(id);

                if (!existsInProduct)
                    _repo.Delete(id);
                else
                {
                    TempData["Alert"] = Alert.ForeignKey;
                    TempData["AlertContent"] = category.Name;
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }   
        }
    }
}
