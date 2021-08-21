using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnicalTest.Models;

namespace TechnicalTest.Services
{
    public class ProductsRepository
    {
        public List<Product> Get(bool withCategories)
        {
            using (var db = new ApplicationDbContext())
            {
                if (withCategories)
                {
                    return db.Products.Include("Category").ToList();
                }
                return db.Products.ToList();
            }
        }
        public Product Get(int id, bool withCategories)
        {
            using (var db = new ApplicationDbContext())
            {
                if (withCategories)
                    return db.Products.Include("Category").FirstOrDefault(p => p.Id == id);
                return db.Products.Find(id);
            }
        }
        public void Add(Product product)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
        }
        public void Edit(Product product)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }

        public static List<SelectListItem> GetCategoriesDropdownList()
        {
            CategoriesRepository _repoCategories = new CategoriesRepository();
            var categoriesList = _repoCategories.Get();
            var categoriesSelectList = CategoriesRepository.ConvertToDropDownList(categoriesList);
            return categoriesSelectList;
        }
    }
}