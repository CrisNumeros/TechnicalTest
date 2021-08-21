using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnicalTest.Models;

namespace TechnicalTest.Services
{
    public class CategoriesRepository
    {
        public List<Category> Get()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Categories.ToList();
            }
        }
        public Category Get(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Categories.Find(id);
            }
        }
        public void Add(Category category)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }
        public void Edit(Category category)
        {
            using(var db = new ApplicationDbContext())
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                Category category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }
        public bool ExistsInProduct(int idCategory)
        {
            using (var db = new ApplicationDbContext())
            {
                int count = db.Products.Include("Category").Where(p => p.Category.Id == idCategory).Count();
                return count > 0;
            }
        }

        public static List<SelectListItem> ConvertToDropDownList(List<Category> list)
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            foreach (var element in list)
            {
                returnList.Add(new SelectListItem()
                {
                    Value = element.Id.ToString(),
                    Text = element.Name
                });
            }
            return returnList;
        }
    }
}