using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnicalTest.Models;
using TechnicalTest.Services;

namespace TechnicalTest.Controllers
{
    public class ProductsController : Controller
    {
        private ProductsRepository _repo = new ProductsRepository();

        private enum Alert
        {
            DontExists
        }


        // GET: Products
        public ActionResult Index()
        {
            if (TempData.ContainsKey("Alert"))
            {
                Alert alert = (Alert)TempData["Alert"];
                switch (alert)
                {
                    case Alert.DontExists:
                        Utils.SetAlert(ViewBag, "There are no categories.", Utils.AlertColors.danger);
                        break;
                    default:
                        break;
                }
            }

            List<Product> product = _repo.Get(true);
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var categories = ProductsRepository.GetCategoriesDropdownList();
            if (categories.Count == 0)
            {
                TempData["Alert"] = Alert.DontExists;
                return RedirectToAction("Index");
            }

            ViewBag.Categories = categories;
            ViewBag.IsEditing = false;

            return View("CreateAndEdit");
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,Description,IdCategory")]Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("ModelState not valid.");

                _repo.Add(product);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.IsEditing = false;
                ViewBag.Categories = ProductsRepository.GetCategoriesDropdownList();
                return View("CreateAndEdit", product);
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _repo.Get(id.Value, true);
            if (product == null)
            {
                return HttpNotFound();
            }

            var categories = ProductsRepository.GetCategoriesDropdownList();
            if (categories.Count == 0)
            {
                TempData["Alert"] = Alert.DontExists;
                return RedirectToAction("Index");
            }

            ViewBag.IsEditing = true;
            ViewBag.Categories = categories;
            return View("CreateAndEdit", product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,IdCategory")] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("ModelState not valid.");
                
                _repo.Edit(product);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.IsEditing = true;
                ViewBag.Categories = ProductsRepository.GetCategoriesDropdownList();
                return View("CreateAndEdit", product);
            }
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Product product = _repo.Get(id, false);
                if (product == null)
                {
                    return HttpNotFound();
                }

                _repo.Delete(id);
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }
    }
}
